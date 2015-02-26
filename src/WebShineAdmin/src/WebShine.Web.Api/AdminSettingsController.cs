using System;
using System.Net.Http;
using System.Web.Http;
using WebShine.Web.Api.Models.Dashboard;

namespace WebShine.Web.Api {

	[RoutePrefix("admin/api/settings")]
	public class AdminSettingsController : BaseApiController {

		[Route("")]
		public HttpResponseMessage Get() {
			return this.Request.CreateResponse<SolutionSettings>(new SolutionSettings { InstallationName = "News Untold", Environment = "Development", Languages = new BackEndLanguage[] { new BackEndLanguage { Name = "en-GB", Label = "English" }, new BackEndLanguage { Name = "da-DK", Label = "Danish" } } });
		}
	}
}