using Microsoft.AspNet.Builder;
using System;

namespace WebShine.Admin.Builder {

	public static class BuilderExtensions {

		public static IApplicationBuilder UseWebShine(this IApplicationBuilder app) {
			return app;
		}
	}
}