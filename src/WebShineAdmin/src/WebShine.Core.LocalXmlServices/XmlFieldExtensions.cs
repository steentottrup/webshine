using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WebShine.Core.Abstracts;

namespace WebShine.Core.LocalXmlServices {

	public static class XmlFieldExtensions {

		public static void Populate(this IField field, XElement fieldNode, String language) {
			field.Type = fieldNode.Attribute("type").Value;
			field.SortOrder = Int32.Parse(fieldNode.Attribute("sortOrder").Value);
			field.CustomProperties = fieldNode.Element("customProperties") == null ? new Dictionary<String, Object>() : fieldNode.Element("customProperties").Elements("add").Select(c => new KeyValuePair<String, Object>(c.Attribute("name").Value, c.Value)).ToDictionary(k => k.Key, v => v.Value);

			field.LocalizedTexts = new Dictionary<String, String>();
			if (fieldNode.Element("labels")!=null) {
				foreach (XElement labelNode in fieldNode.Element("labels").Elements("label")) {
					if (labelNode.Attribute("language") == null) {
						field.LocalizedTexts[String.Empty] = labelNode.Value;
					}
					else {
						field.LocalizedTexts[labelNode.Attribute("language").Value] = labelNode.Value;
					}
				}
			}
			field.Namespace = fieldNode.Attribute("namespace") == null ? String.Empty : fieldNode.Attribute("namespace").Value;
			field.Key = fieldNode.Attribute("key") == null ? String.Empty : fieldNode.Attribute("key").Value;

			//field.DataType = GetAllowedType(fieldNode.Attribute("dataType").Value, fieldNode.Attribute("multipleValues") == null ? false : Boolean.Parse(fieldNode.Attribute("multipleValues").Value));
			field.DataType = fieldNode.Attribute("dataType").Value;

			Boolean editable = false;
			field.Editable = fieldNode.Attribute("editable") != null && Boolean.TryParse(fieldNode.Attribute("editable").Value, out editable) ? editable : true;
			Boolean required = false;
			field.Required = fieldNode.Attribute("required") != null && Boolean.TryParse(fieldNode.Attribute("required").Value, out required) ? required : false;
			Boolean hidden = false;
			field.Hidden = fieldNode.Attribute("hidden") != null && Boolean.TryParse(fieldNode.Attribute("hidden").Value, out hidden) ? hidden : false;
		}

		private static Boolean TryGetLabel(XElement parent, String language, out XElement labelNode) {
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
