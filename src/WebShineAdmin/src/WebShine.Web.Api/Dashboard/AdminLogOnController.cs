using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;
using WebShine.Web.Api.Models;

namespace WebShine.Web.Api.Dashboard {

	[RoutePrefix("admin/api/dashboard/account")]
	public class AdminLogOnController : BaseApiController {

		[Route("logon")]
		public HttpResponseMessage Post(LogOn model) {
			if (ModelState.IsValid) {
				// TODO:
				if (model.Username == "steen" && model.Password == "123456") {
					//await SignInAsync(new User { Name = "Steen" }, false);
					FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
					return this.Request.CreateResponse<Boolean>(true);
				}

				return this.Request.CreateResponse<Boolean>(false);
			}

			return this.Request.CreateResponse<Error>(HttpStatusCode.BadRequest, this.GetModelStateErrors());
		}

		//private async Task SignInAsync(User user, Boolean isPersistent) {
		//	AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
		//	var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
		//	AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
		//}

		[Route("logoff")]
		public HttpResponseMessage Put() {
			FormsAuthentication.SignOut();
			return this.Request.CreateResponse(HttpStatusCode.OK);
		}
	}
}