using System;
using System.Collections.Generic;

namespace WebShine.Core.Abstracts {

	public interface ITab {
		String Name { get; set; }
		String Label { get; set; }
		List<IField> Fields { get; set; }
		Int32 SortOrder { get; set; }
	}
}