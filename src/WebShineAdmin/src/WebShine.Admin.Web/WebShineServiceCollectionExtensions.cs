using Microsoft.AspNet.Mvc;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;

namespace WebShine.Admin.DependencyInjection {

	public static class MvcServiceCollectionExtensions {

		public static IServiceCollection AddWebShine(this IServiceCollection services, IConfiguration configuration = null) {
			services.Configure<MvcOptions>(
				options => options.OutputFormatters.RemoveAll(
					formatter => formatter.Instance is XmlDataContractSerializerOutputFormatter));

			if (configuration != null) {
				services.AddInstance<IConfiguration>(configuration);
				String templateCollection = configuration.Get("WebShine:Template:CollectionService:Type");
				if (!String.IsNullOrWhiteSpace(templateCollection)) {
					// TODO:
				}
			}
			else {
				// TODO: Default config
			}


			return services;
		}
	}
}