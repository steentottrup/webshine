using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using WebShine.Admin.DependencyInjection;
using WebShine.Admin.Builder;
using Microsoft.Framework.ConfigurationModel;
using System.IO;

namespace WebShineAdmin {

	public class Startup {

		public IConfiguration Configuration { get; set; }

		public Startup() {
			this.Configuration = new Configuration()
				.AddJsonFile(Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "configuration\\config.json"))
				.AddEnvironmentVariables();
		}

		public void ConfigureServices(IServiceCollection services) {
			services.AddWebShine(this.Configuration);
		}

		public void Configure(IApplicationBuilder app) {
			app.UseWebShine();
		}
	}
}