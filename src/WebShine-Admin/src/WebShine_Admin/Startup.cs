using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.ConfigurationModel;
using System.IO;
using WebShine.Admin.DependencyInjection;
using WebShine.Admin.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using Microsoft.Framework.Runtime;

namespace WebShine_Admin {

	//public class Database {
	//	public String Type { get; set; }
	//	//public String ConnectString { get; set; }
	//}

	public class Startup {
		public IConfiguration Configuration { get; set; }
		public ILogger Logger { get; set; }

		public Startup(IApplicationEnvironment env, ILoggerFactory loggerFactory) {
			this.Logger = loggerFactory.Create("Init");
			this.Configuration = new Configuration()
				.AddJsonFile(Path.Combine(env.ApplicationBasePath, "wwwroot", "app_data", "configuration\\config.json"))
				.AddEnvironmentVariables();
		}

		public void ConfigureServices(IServiceCollection services) {
			services.AddMvc();
			services.AddWebShine(this.Configuration, this.Logger);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
			loggerFactory.AddConsole();

			app.UseMvc(routes => {
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action}/{id?}",
					defaults: new { controller = "Home", action = "Index" });
			});

			app.UseWebShine();
		}
	}
}