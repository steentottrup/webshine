using System;
using System.Linq;
using WebShine.Core.Abstracts;
using ElementModel = WebShine.Web.Api.Models.Dashboard.Element;

namespace WebShine.Web.Api.Helpers {

	public static class ElementHelper {

		public static ElementModel Populate(IElement element) {
			return new ElementModel {
				Id = element.Id,
				ParentId = element.ParentId,
				Name = element.Name,
				Template = element.Template,
				Tree = element.Tree,
				Properties = element.Properties.Select(p => PropertyHelper.Populate(p)).ToArray()
			};
		}
	}
}
