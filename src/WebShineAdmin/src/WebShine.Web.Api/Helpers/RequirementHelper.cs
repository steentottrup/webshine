using System;
using WebShine.Core.Abstracts;
using RequirementModel = WebShine.Web.Api.Models.Dashboard.Requirement;

namespace WebShine.Web.Api.Helpers {

	public static class RequirementHelper {

		public static RequirementModel Populate(IRequirement requirement, String language) {
			return new RequirementModel {
				Type = requirement.Type,
				Parameter = requirement.Parameter,
				Namespace = requirement.Namespace,
				Key = requirement.Key,
				Text = requirement.GetText(language)
			};
		}
	}
}
