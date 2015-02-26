using System;
using System.Linq;
using WebShine.Core.Abstracts;
using TabModel = WebShine.Web.Api.Models.Dashboard.Tab;

namespace WebShine.Web.Api.Helpers {

	public static class TabHelper {

		public static TabModel Populate(ITab tab, String language) {
			return new TabModel {
				Label = tab.Label,
				Name = tab.Name,
				SortOrder = tab.SortOrder,
				Fields = tab.Fields.Select(f => FieldHelper.Populate(f, language)).ToList()
			};
		}
	}
}