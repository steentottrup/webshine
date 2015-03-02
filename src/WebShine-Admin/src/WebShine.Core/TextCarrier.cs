using System;
using System.Collections.Generic;
using WebShine.Core.Abstracts;

namespace WebShine.Core {

	public class TextCarrier : ITextCarrier {
		public String Namespace { get; set; }
		public String Key { get; set; }
		public IDictionary<String, String> LocalizedTexts { get; set; }
	}
}
