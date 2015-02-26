using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebShine.Web.Api.Models;

namespace WebShine.Web.Api {

	public abstract class BaseApiController : ApiController {

		protected Error GetModelStateErrors() {
			return new Error {
				Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray()
			};
		}

		protected HttpResponseMessage CreateForbiddenResponse() {
			// TODO: Localize!
			return this.Request.CreateResponse<Error>(
															HttpStatusCode.Forbidden,
															new Error {
																Errors = new String[] { "Du har ikke adgang til denne information" }
															});
		}

		protected HttpResponseMessage CreateNotFoundResponse(String elementName) {
			// TODO: Localize!
			return this.Request.CreateResponse<Error>(
															HttpStatusCode.NotFound,
															new Error {
																Errors = new String[] { String.Format("{0} blev ikke fundet", elementName) }
															});
		}
	}
}
