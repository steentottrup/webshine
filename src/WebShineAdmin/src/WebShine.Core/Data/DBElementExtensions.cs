using System;
using System.Linq;
using WebShine.Core.Abstracts;

namespace WebShine.Core.Data {

	public static class DBElementExtensions {

		public static IElement ToElement(this DBElement element, String tree) {
			return new Element {
				Id = element.Id,
				ParentId = element.ParentId,
				Name = element.Name,
				Template = element.Template,
				Tree = tree,
				Properties = element.Properties == null ? new IProperty[] { } : element.Properties.Select(p => new Property { Name = p.Name, Value = p.Value }).ToArray()
			};
		}
	}
}