using System;
using System.Web.Http;

namespace WebShine.Web.Api {

	[Authorize]
	public abstract class BaseAuthenticatedApiController : BaseApiController {
	}
}