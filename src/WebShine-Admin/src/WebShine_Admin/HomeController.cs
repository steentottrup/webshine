using Microsoft.AspNet.Mvc;
using Microsoft.Framework.OptionsModel;
using WebShine.Admin.DependencyInjection;
using WebShine.Core.Abstracts.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShine_Admin {

	public class HomeController : Controller {

		public HomeController(ITemplateCollection templateCollection) {

		}

		public IActionResult Index() {
			return View();
		}
	}
}
