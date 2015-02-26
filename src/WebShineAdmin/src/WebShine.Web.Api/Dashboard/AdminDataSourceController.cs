using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebShine.Web.Api.DataSources;

namespace WebShine.Web.Api.Dashboard {

	[RoutePrefix("admin/api/datasource")]
	public class AdminDataSourceController : BaseAuthenticatedApiController {

		[Route("simple/{type}")]
		public HttpResponseMessage Get(String type) {
			switch (type) {
				case "cultureinfo":
					return this.Request.CreateResponse(new CultureInfoDataSource().Get());
				case "timezone":
					return this.Request.CreateResponse(new TimezoneDataSource().Get());
					break;
				default:
					break;
			}



			return this.Request.CreateErrorResponse(HttpStatusCode.BadGateway, "");
		}
	}
}
