using System;
using System.IO;
using System.Web;
using WebShine.Core.Abstracts;

namespace WebShine.Core.LocalXmlServices {

	public class LocalFileSystemTreeConfigurationService : ITreeConfigurationService {
		private ITreeConfigurationParser parse;

		public LocalFileSystemTreeConfigurationService(ITreeConfigurationParser parse) {
			this.parse = parse;
		}

		public String GetFullPath(String name) {
			// TODO ????
			return Path.Combine(HttpContext.Current.Server.MapPath("~/app_data/configuration/trees"), String.Format("{0}.xml", name));
		}

		public Boolean Exists(String name) {
			return File.Exists(this.GetFullPath(name));
		}

		public ITreeConfiguration Get(String name) {
			if (this.Exists(name)) {
				using (FileStream treeStream = File.OpenRead(this.GetFullPath(name))) {
					return this.parse.Parse(treeStream, this);
				}
			}
			else {
				// TODO:
				throw new ApplicationException("not found");
			}
		}
	}
}