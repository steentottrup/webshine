using System;
using System.Net.Http;

namespace WebShine.Web.Api.Dashboard {

	[RoutePrefix("admin/api/dashboard/tree")]
	public class AdminTreeController : BaseAuthenticatedApiController {

		[Route("{type}/{id}")]
		public HttpResponseMessage Get(String type, String id) {

			return this.CreateNotFoundResponse(type);
		}
	}
}
