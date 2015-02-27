using System;
using System.Collections.Generic;
using WebShine.Core.Abstracts;

namespace WebShine.Core {

	public class Template : ITemplate {
		public String Name { get; set; }
		public List<ITab> Tabs { get; set; }
		public String CacheKey {
			get {
				return String.Format("template.{0}", this.Name);
			}
		}
	}
}
