using SimpleLocalisation;
using SimpleLocalisation.Web;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebShine.Web.Api.Dashboard {

	[RoutePrefix("admin/api/dashboard/text")]
	public class AdminTextController : BaseApiController {

		[Route("{ci}")]
		public HttpResponseMessage Get(String ci) {
			TextManager manager = new TextManager(new WebCultureContext(), new XmlFileTextSource(
					() => HttpContext.Current.Server.MapPath("~/app_data/texts")
				), new Language[] { new Language("en-GB") });

			String hack = manager.Get("hans");

			return this.Request.CreateResponse<LocalisedText[]>(manager.Texts.ToArray());
		}
	}
}