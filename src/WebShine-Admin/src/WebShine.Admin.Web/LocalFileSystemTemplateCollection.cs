using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.Runtime;
using System;
using System.IO;
using System.Text;
using WebShine.Core.Abstracts.Services;

namespace WebShine.Admin.Web {

	public class LocalFileSystemTemplateCollection : ITemplateCollection {
		protected readonly String rootFolder;
		protected readonly String extension;
		private const String configRoot = "WebShine:ContentManager:LocalFileSystemTemplateCollection";

		public LocalFileSystemTemplateCollection(IApplicationEnvironment env, IConfiguration config) {
			String templateFolder = config.Get(configRoot + ":Root");
			this.rootFolder = Path.Combine(env.ApplicationBasePath, "wwwroot", templateFolder);
			this.extension = config.Get(config + ":Extension");
		}

		public Stream Get(String name) {
			return new MemoryStream(
				Encoding.UTF8.GetBytes(
					File.ReadAllText(Path.Combine(rootFolder, String.Format("{0}.{1}", name, extension)))
				));
		}

		public String[] GetAll() {
			return Directory.GetFiles(rootFolder, String.Format("*.{0}", extension));
		}

		public Boolean Exists(String name) {
			return File.Exists(Path.Combine(rootFolder, String.Format("{0}.{1}", name, extension)));
		}
	}
}