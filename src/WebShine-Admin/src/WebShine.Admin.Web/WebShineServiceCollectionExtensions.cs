using Microsoft.AspNet.Mvc;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;
using System;
using System.Reflection;
using WebShine.Core.Abstracts.Services;

namespace WebShine.Admin.DependencyInjection {

	//public class DataConfig {
	//	public String Type { get; set; }
	//	//public String ConnectString { get; set; }
	//}

	public static class MvcServiceCollectionExtensions {

		public static IServiceCollection AddWebShine(this IServiceCollection services, IConfiguration configuration = null, ILogger logger = null) {
			// Let's remove the xml output option for web api
			services.Configure<MvcOptions>(
				options => options.OutputFormatters.RemoveAll(
					formatter => formatter.Instance is XmlDataContractSerializerOutputFormatter));

			if (configuration == null) {
			}
			else {
				services.AddInstance<IConfiguration>(configuration);

				// TODO: huh??
				//services.Configure<IOptions<DataConfig>>(configuration, "Database");

				String templateCollectionString = configuration.Get("WebShine:ContentManager:Services:TemplateCollection");
				if (String.IsNullOrWhiteSpace(templateCollectionString)) {
					logger.WriteError("No Template Collection type given");
				}
				Type templateCollection = null;
				if (!TryGetType(templateCollectionString, out templateCollection)) {
					logger.WriteError("Could not get template collection");
				}

				services.AddTransient(typeof(ITemplateCollection), templateCollection);
			}


			return services;
		}
		private static Boolean TryGetType(String typeString, out Type type) {
			type = null;
			String[] parts = typeString.Split(new Char[] { ','}, StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length == 2) {
				try {
					Assembly ass = Assembly.Load(parts[1]);
					type = ass.GetType(parts[0]);

					return true;
				}
				catch { }
			}

			return false;
		}
	}
}