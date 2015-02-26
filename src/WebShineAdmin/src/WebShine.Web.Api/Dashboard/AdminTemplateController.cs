using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using WebShine.Core.Abstracts;
using WebShine.Core.LocalXmlServices;
using WebShine.Web.Api.Helpers;

namespace WebShine.Web.Api.Dashboard {

	[RoutePrefix("admin/api/template")]
	public class AdminTemplateController : BaseAuthenticatedApiController {

		[Route("{name}")]
		public HttpResponseMessage Get(String name) {
			// TODO:
			String language = Thread.CurrentThread.CurrentUICulture.Name;

			ITemplateService service = new LocalFileSystemTemplateService(new XmlTemplateParser(), new MemoryCacheService());
			if (service.Exists(name)) {
				// TODO: TemplateModel ?!?!?!
				return this.Request.CreateResponse(HttpStatusCode.OK, TemplateHelper.Populate(service.Get(name), language));
			}
			return this.CreateNotFoundResponse("template");
		}
	}
}