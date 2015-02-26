using System;
using System.Net.Http;
using System.Web.Http;
using WebShine.Web.Api.Models.Dashboard;

namespace WebShine.Web.Api.Dashboard {

	[RoutePrefix("admin/api/dashboard/menu")]
	public class AdminMenuController : BaseAuthenticatedApiController {

		[Route("")]
		public HttpResponseMessage Get() {
			return this.Request.CreateResponse<LeftMenu>(
					new LeftMenu {
						Namespace = "WebShine.Web.UI.Dashboard.LeftMenu",
						Key = "Main",
						Icon = "fa fa-reorder",
						Items = new MenuItem[] { 
							new MenuItem { Title = String.Empty, Namespace = "WebShine.Web.UI.Dashboard.LeftMenu", Key = "Site", Icon = "fa fa-align-left", Link = "site" },
							//new MenuItem { Title = String.Empty, Namespace = "WebShine.Web.UI.Dashboard.LeftMenu", Key = "Content", Icon = "fa fa-align-right", Link = "#", Items = new MenuItem[] {
							//	new MenuItem { Title = "Level 2", Icon = "fa fa-user", Link = "#", Items = new MenuItem[] {
							//		new MenuItem { Title = "Templates", Icon = "fa fa-columns", Link = "template" }
							//	} }
							//} },
							new MenuItem { Title = String.Empty, Namespace = "WebShine.Web.UI.Dashboard.LeftMenu", Key = "Files", Icon = "fa fa-files-o", Link = "file" },
							new MenuItem { Title = String.Empty, Namespace = "WebShine.Web.UI.Dashboard.LeftMenu", Key = "Users", Icon = "fa fa-user", Link = "user" }
						}
					});
		}
	}
}