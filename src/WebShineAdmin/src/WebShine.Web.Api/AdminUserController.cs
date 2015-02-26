using MongoDB.Bson;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using WebShine.Web.Api.Models;

namespace WebShine.Web.Api {

	[RoutePrefix("admin/api/user")]
	public class AdminUserController : BaseAuthenticatedApiController {

		[Route("")]
		public HttpResponseMessage Get() {
			if (Thread.CurrentPrincipal.Identity.IsAuthenticated) {
				// TODO:
				return this.Request.CreateResponse<User>(HttpStatusCode.OK,
						new User { Authenticated = true, Name = Thread.CurrentPrincipal.Identity.Name, Language = "en-GB", Id = ObjectId.GenerateNewId()
					});
			}
			return this.CreateForbiddenResponse();
		}
	}
}