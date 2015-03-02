using System;
using System.Collections.Generic;

namespace WebShine.Core.Abstracts {

	public interface ITemplate {
		String Name { get; set; }
		List<ITab> Tabs { get; set; }
		String CacheKey { get; }
	}
}