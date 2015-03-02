using System;
using System.Linq;
using MongoDB.Bson;
using WebShine.Core.Abstracts;

namespace WebShine.Core.Data {

	public static class DBElementExtensions {

		public static IElement ToElement(this DBElement element, String tree) {
			return new Element {
				Id = element.Id,
				Name = element.Name,
				ParentId = element.ParentId,
				Template = element.Template,
				Tree = tree,
				Properties = element.Properties == null ? new IProperty[] { } : element.Properties.Select(p => new Property { Name = p.Name, Value = p.Value }).ToArray()
			};
		}

		public static Int32 SortOrder(this DBElement element, ITemplate template) {
			if (element.Template != template.Name) {
				throw new ArgumentException("not correct template");
			}
			IField field = template.GetSortOrderField();
			BsonElement e = element.Properties.FirstOrDefault(p => p.Name == field.Name);
			return e != null ? e.Value.AsNullableInt32.Value : -1;
		}

		public static void SetSortOrder(this DBElement element, ITemplate template, Int32 sortOrder) {
			if (element.Template != template.Name) {
				throw new ArgumentException("not correct template");
			}
			IField field = template.GetSortOrderField();
			if (field == null) {
				// TODO:
				throw new ApplicationException("not sortable");
			}

			BsonElement property = element.Properties.FirstOrDefault(p => p.Name == field.Name);
			//if (property == null) {
			//	element.Properties.Add(new BsonElement(field.Name, sortOrder));
			//}
			//else {
			//	property.Value = sortOrder;
			//}
		}
	}
}
