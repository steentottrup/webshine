using System;
using System.Collections.Generic;
using System.Linq;
using WebShine.Core.Abstracts;
using FieldModel = WebShine.Web.Api.Models.Dashboard.Field;

namespace WebShine.Web.Api.Helpers {

	public static class FieldHelper {
		private static Dictionary<Type, String> types = new Dictionary<Type, String>();

		public static FieldModel Populate(IField field, String language) {
			return new FieldModel {
				Type = field.Type,
				SortOrder = field.SortOrder,
				Required = field.Required,
				Name = field.Name,
				Label = field.GetText(language),
				Namespace = field.Namespace,
				Key = field.Key,
				Editable = field.Editable,
				Hidden = field.Hidden,
				DataType = "", //types[field.DataType],
				CustomProperties = field.CustomProperties,
				Requirements = field.Requirements.Select(r => RequirementHelper.Populate(r, language)).ToList()
			};
		}
	}
}
