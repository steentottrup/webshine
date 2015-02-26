using System;
using WebShine.Core.Abstracts;
using PropertyModel = WebShine.Web.Api.Models.Dashboard.Property;

namespace WebShine.Web.Api.Helpers {

	public static class PropertyHelper {

		public static PropertyModel Populate(IProperty property) {
			return new PropertyModel {
				//Editable = property.Editable,
				Name = property.Name,
				//Type = property.Type,
				Value = property.Value
			};
		}
	}
}
