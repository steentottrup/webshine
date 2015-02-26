using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WebShine.Core.Abstracts;

namespace WebShine.Core.LocalXmlServices {

	public class XmlTreeConfigurationParser : ITreeConfigurationParser {

		public ITreeConfiguration Parse(Stream stream, ITreeConfigurationService service) {
			XDocument xml = XDocument.Load(stream);

			ITreeConfiguration tree = new TreeConfiguration();
			tree.Name = xml.Root.Attribute("name").Value;

			tree.RootTemplates = xml.Root.Element("root").Elements("template").Select(t => t.Attribute("name").Value).ToList();
			tree.AllowedTemplates = xml.Root.Element("templates").Elements("template").Select(t => new TemplateRelation { Template = t.Attribute("name").Value, ChildTemplates = t.Element("allowedChildren") == null ? new String[] { } : t.Element("allowedChildren").Elements("template").Select(t2 => t2.Attribute("name").Value).ToArray() }).ToList();
			// TODO:

			return tree;
		}
	}
}
