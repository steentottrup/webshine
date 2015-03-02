using System;
using System.Collections.Generic;
using WebShine.Core.Abstracts;

namespace WebShine.Core {

	public class Field : TextCarrier, IField {

		public Field() {
			this.Editable = true;
		}

		public Int32 SortOrder { get; set; }
		public String Name { get; set; }
		public String Type { get; set; }
		public Boolean Editable { get; set; }
		public Boolean Required { get; set; }
		public Boolean Hidden { get; set; }

		public Dictionary<String, Object> CustomProperties { get; set; }

		public String DataType { get; set; }

		public List<IRequirement> Requirements { get; set; }
	}
}
