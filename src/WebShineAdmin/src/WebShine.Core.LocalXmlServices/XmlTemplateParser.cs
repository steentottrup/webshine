using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using WebShine.Core.Abstracts;

namespace WebShine.Core.LocalXmlServices {

	public class XmlTemplateParser : ITemplateParser {

		public ITemplate Parse(Stream stream, ITemplateService service) {
			XDocument xml = XDocument.Load(stream);

			ITemplate tmp = null;

			if (xml.Root.Attribute("inherit") != null && !String.IsNullOrWhiteSpace(xml.Root.Attribute("inherit").Value)) {
				tmp = service.Get(xml.Root.Attribute("inherit").Value);
			}
			else {
				tmp = new Template();
				tmp.Tabs = new List<ITab>();
			}
			tmp.Name = xml.Root.Attribute("name").Value;

			// TODO: Language from context!!
			String ci = Thread.CurrentThread.CurrentUICulture.Name;

			if (xml.Root.Element("tabs") != null) {
				foreach (XElement tabNode in xml.Root.Element("tabs").Elements("tab")) {
					if (!tmp.Tabs.Any(t => t.Name == tabNode.Attribute("name").Value)) {
						tmp.Tabs.Add(new Tab { Name = tabNode.Attribute("name").Value, Fields = new List<IField>() });
					}
					ITab tab = tmp.Tabs.First(t => t.Name == tabNode.Attribute("name").Value);

					XElement labelNode;
					if (this.TryGetLabel(tabNode, ci, out labelNode)) {
						tab.Label = labelNode.Value;
					}
					tab.SortOrder = Int32.Parse(tabNode.Attribute("sortOrder").Value);
				}

				foreach (XElement tabNode in xml.Root.Element("tabs").Elements("tab")) {
					String tabName = tabNode.Attribute("name").Value;

					foreach (XElement fieldNode in tabNode.Element("fields").Elements("field")) {
						String fieldName = fieldNode.Attribute("name").Value;

						IField field = tmp.Tabs.SelectMany(t => t.Fields.Where(f => f.Name == fieldName)).FirstOrDefault();
						if (field != null) {
							ITab tab = tmp.Tabs.FirstOrDefault(t => t.Fields.FirstOrDefault(f => f.Name == fieldName) != null);
							tab.Fields.Remove(field);
						}
						else {
							field = new Field {
								Name = fieldNode.Attribute("name").Value
							};
						}
						field.Populate(fieldNode, ci);

						tmp.Tabs.First(t => t.Name == tabName).Fields.Add(field);

						field.Requirements = new List<IRequirement>();
						if (fieldNode.Element("requirements") != null) {
							foreach (XElement requirementNode in fieldNode.Element("requirements").Elements("requirement")) {
								Requirement req = new Requirement {
									Type = requirementNode.Attribute("type").Value,
									Parameter = requirementNode.Element("parameter") != null ? requirementNode.Element("parameter").Value : String.Empty,
									Key = requirementNode.Attribute("key") != null ? requirementNode.Attribute("key").Value : String.Empty,
									Namespace = requirementNode.Attribute("namespace") != null ? requirementNode.Attribute("namespace").Value : String.Empty,
									LocalizedTexts = new Dictionary<String, String>()
								};

								if (requirementNode.Element("texts") != null) {
									foreach (XElement labelNode in requirementNode.Element("texts").Elements("text")) {
										if (labelNode.Attribute("language") == null) {
											req.LocalizedTexts[String.Empty] = labelNode.Value;
										}
										else {
											req.LocalizedTexts[labelNode.Attribute("language").Value] = labelNode.Value;
										}
									}
								}

								field.Requirements.Add(req);
							}
						}
					}
				}
			}
			else if (tmp.Tabs == null) {
				tmp.Tabs = new List<ITab>();
			}

			return tmp;
		}

		private Boolean TryGetLabel(XElement parent, String language, out XElement labelNode) {
			labelNode = null;
			if (parent.Elements("label").Any(l => l.Attribute("language") != null && l.Attribute("language").Value == language)) {
				labelNode = parent.Elements("label").First(l => l.Attribute("language") != null && l.Attribute("language").Value == language);
			}
			if (parent.Elements("label").Any(l => l.Attribute("language") == null && !String.IsNullOrWhiteSpace(l.Value))) {
				labelNode = parent.Elements("label").First(l => l.Attribute("language") == null && !String.IsNullOrWhiteSpace(l.Value));
			}
			return labelNode != null;
		}
	}
}