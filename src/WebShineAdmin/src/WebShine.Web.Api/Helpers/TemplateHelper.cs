using System;
using System.Linq;
using WebShine.Core;
using WebShine.Core.Abstracts;
using TemplateModel = WebShine.Web.Api.Models.Dashboard.Template;

namespace WebShine.Web.Api.Helpers {

	public static class TemplateHelper {

		public static TemplateModel Populate(ITemplate template, String language) {
			return new TemplateModel {
				Name = template.Name,
				Tabs = template.Tabs.Select(t => TabHelper.Populate(t, language)).ToList()
			};
		}
	}
}
