using System;
using System.Collections.Generic;

namespace WebShine.Core.Abstracts {

	public interface ITextCarrier {
		String Namespace { get; set; }
		String Key { get; set; }
		IDictionary<String, String> LocalizedTexts { get; set; }
	}
}
