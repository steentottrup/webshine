//using System;
//using System.IO;
//using System.Web;
//using WebShine.Core.Abstracts;

//namespace WebShine.Web.Api.Dashboard {

//	public class LocalFileSystemTemplateService : ITemplateService {
//		private ITemplateParser parse;

//		public LocalFileSystemTemplateService(ITemplateParser parse) {
//			this.parse = parse;
//		}

//		private String GetFullPath(String name) {
//			return Path.Combine(HttpContext.Current.Server.MapPath("~/app_data/configuration/templates"), String.Format("{0}.xml", name));
//		}

//		public Boolean Exists(String name) {
//			return File.Exists(this.GetFullPath(name));
//		}

//		public ITemplate Get(String name) {
//			if (this.Exists(name)) {
//				using (FileStream templateStream = File.OpenRead(this.GetFullPath(name))) {
//					return this.parse.Parse(templateStream, this);
//				}
//			}
//			else {
//				// TODO:
//				throw new ApplicationException("not found");
//			}
//		}
//	}
//}