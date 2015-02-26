using System;

namespace WebShine.Web.Api.Models.Dashboard {

	public class SolutionSettings {
		public String InstallationName { get; set; }
		public String Environment { get; set; }
		public BackEndLanguage[] Languages { get; set; }
	}
}