using System;
using System.IO;
using System.Web;
using WebShine.Core.Abstracts;

namespace WebShine.Core.LocalXmlServices {

	public class LocalFileSystemTemplateService : BaseTemplateService {
		private ITemplateParser parse;

		public LocalFileSystemTemplateService(ITemplateParser parse, ICacheService cache) : base(cache) {
			this.parse = parse;
		}

		private String GetFullPath(String name) {
			// TODO ????
			return Path.Combine(HttpContext.Current.Server.MapPath("~/app_data/configuration/templates"), String.Format("{0}.xml", name));
		}

		public override Boolean Exists(String name) {
			return File.Exists(this.GetFullPath(name));
		}

		public override ITemplate Get(String name) {
			ITemplate template = this.GetFromCache(name);
			if (template == null) {
				if (this.Exists(name)) {
					using (FileStream templateStream = File.OpenRead(this.GetFullPath(name))) {
						template = this.parse.Parse(templateStream, this);
					}
					this.cache.Put(template.CacheKey, template);
				}
				else {
					// TODO:
					throw new ApplicationException("not found");
				}
			}
			return template;
		}
	}
}
