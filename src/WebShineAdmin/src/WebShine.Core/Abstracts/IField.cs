using System;
using System.Collections.Generic;

namespace WebShine.Core.Abstracts {

	public interface IField : ITextCarrier {
		Int32 SortOrder { get; set; }
		String Name { get; set; }
		String Type { get; set; }
		Boolean Editable { get; set; }
		Boolean Required { get; set; }
		Boolean Hidden { get; set; }
		Dictionary<String, Object> CustomProperties { get; set; }
		//Type DataType { get; set; }
		String DataType { get; set; }
		List<IRequirement> Requirements { get; set; }
	}
}