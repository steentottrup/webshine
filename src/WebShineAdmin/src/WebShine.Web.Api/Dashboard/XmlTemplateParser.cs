//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Xml.Linq;
//using MongoDB.Bson;
//using WebShine.Core;
//using WebShine.Core.Abstracts;

//namespace WebShine.Web.Api.Dashboard {

//	public class XmlTemplateParser : ITemplateParser {

//		public ITemplate Parse(Stream stream, ITemplateService service) {
//			XDocument xml = XDocument.Load(stream);

//			ITemplate tmp = null;

//			if (xml.Root.Attribute("inherit") != null && !String.IsNullOrWhiteSpace(xml.Root.Attribute("inherit").Value)) {
//				tmp = service.Get(xml.Root.Attribute("inherit").Value);
//			}
//			else {
//				tmp = new Template();
//				tmp.Tabs = new List<ITab>();
//			}
//			tmp.Name = xml.Root.Attribute("name").Value;

//			// TODO: Language from context!!
//			String ci = "en-GB";

//			foreach (XElement tabNode in xml.Root.Element("tabs").Elements("tab")) {
//				if (!tmp.Tabs.Any(t => t.Name == tabNode.Attribute("name").Value)) {
//					tmp.Tabs.Add(new Tab { Name = tabNode.Attribute("name").Value, Fields = new List<IField>() });
//				}
//				ITab tab = tmp.Tabs.First(t => t.Name == tabNode.Attribute("name").Value);

//				XElement labelNode;
//				if (this.TryGetLabel(tabNode, ci, out labelNode)) {
//					tab.Label = labelNode.Value;
//				}
//				tab.SortOrder = Int32.Parse(tabNode.Attribute("sortOrder").Value);
//			}

//			foreach (XElement tabNode in xml.Root.Element("tabs").Elements("tab")) {
//				String tabName = tabNode.Attribute("name").Value;

//				foreach (XElement fieldNode in tabNode.Element("fields").Elements("field")) {
//					String fieldName = fieldNode.Attribute("name").Value;

//					IField field = tmp.Tabs.SelectMany(t => t.Fields.Where(f => f.Name == fieldName)).FirstOrDefault();
//					if (field != null) {
//						ITab tab = tmp.Tabs.FirstOrDefault(t => t.Fields.FirstOrDefault(f => f.Name == fieldName) != null);
//						tab.Fields.Remove(field);
//					}
//					else {
//						field = new Field {
//							Name = fieldNode.Attribute("name").Value
//						};
//					}
//					field.Populate(fieldNode, ci);

//					tmp.Tabs.First(t => t.Name == tabName).Fields.Add(field);
//				}
//			}

//			return tmp;
//		}

//		private Boolean TryGetLabel(XElement parent, String language, out XElement labelNode) {
//			labelNode = null;
//			if (parent.Elements("label").Any(l => l.Attribute("language") != null && l.Attribute("language").Value == language)) {
//				labelNode = parent.Elements("label").First(l => l.Attribute("language") != null && l.Attribute("language").Value == language);
//			}
//			if (parent.Elements("label").Any(l => l.Attribute("language") == null && !String.IsNullOrWhiteSpace(l.Value))) {
//				labelNode = parent.Elements("label").First(l => l.Attribute("language") == null && !String.IsNullOrWhiteSpace(l.Value));
//			}
//			return labelNode != null;
//		}
//	}

//	public static class XmlFieldExtensions {

//		public static void Populate(this IField field, XElement fieldNode, String language) {
//			field.Type = fieldNode.Attribute("type").Value;
//			field.SortOrder = Int32.Parse(fieldNode.Attribute("sortOrder").Value);
//			field.CustomProperties = fieldNode.Element("customProperties") == null ? new Dictionary<String, Object>() : fieldNode.Element("customProperties").Elements("add").Select(c => new KeyValuePair<String, Object>(c.Attribute("name").Value, c.Value)).ToDictionary(k => k.Key, v => v.Value);
//			XElement labelNode;
//			if (TryGetLabel(fieldNode, language, out labelNode)) {
//				field.Label = labelNode.Value;
//			}

//			//field.DataType = GetAllowedType(fieldNode.Attribute("dataType").Value, fieldNode.Attribute("multipleValues") == null ? false : Boolean.Parse(fieldNode.Attribute("multipleValues").Value));
//			field.DataType = fieldNode.Attribute("dataType").Value;

//			Boolean editable = false;
//			field.Editable = fieldNode.Attribute("editable") != null && Boolean.TryParse(fieldNode.Attribute("editable").Value, out editable) ? editable : true;
//			Boolean required = false;
//			field.Required = fieldNode.Attribute("required") != null && Boolean.TryParse(fieldNode.Attribute("required").Value, out required) ? required : false;
//			Boolean hidden = false;
//			field.Hidden = fieldNode.Attribute("hidden") != null && Boolean.TryParse(fieldNode.Attribute("hidden").Value, out hidden) ? hidden : false;
//		}

//		//private static Type GetAllowedType(String key, Boolean array) {
//		//	switch (key) {
//		//		case "string":
//		//			return array ? typeof(String[]) : typeof(String);
//		//		case "id":
//		//			return array ? typeof(ObjectId[]) : typeof(ObjectId);
//		//		case "int":
//		//			return array ? typeof(Int32?[]) : typeof(Int32?);
//		//		case "bigint":
//		//			return array ? typeof(Int64?[]) : typeof(Int64?);
//		//		case "datetime":
//		//			return array ? typeof(DateTime?[]) : typeof(DateTime?);
//		//		default:
//		//			throw new ArgumentException("key");
//		//	}
//		//}

//		private static Boolean TryGetLabel(XElement parent, String language, out XElement labelNode) {
//			labelNode = null;
//			if (parent.Elements("label").Any(l => l.Attribute("language") != null && l.Attribute("language").Value == language)) {
//				labelNode = parent.Elements("label").First(l => l.Attribute("language") != null && l.Attribute("language").Value == language);
//			}
//			if (parent.Elements("label").Any(l => l.Attribute("language") == null && !String.IsNullOrWhiteSpace(l.Value))) {
//				labelNode = parent.Elements("label").First(l => l.Attribute("language") == null && !String.IsNullOrWhiteSpace(l.Value));
//			}
//			return labelNode != null;
//		}
//	}
//}
