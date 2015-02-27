using System;
using System.Collections.Generic;
using WebShine.Core.Abstracts;

namespace WebShine.Core {

	public class Tab : ITab {
		public String Name { get; set; }
		public String Label { get; set; }
		public List<IField> Fields { get; set; }
		public Int32 SortOrder { get; set; }
	}
}
