using System;
using System.Collections.Generic;
using WebShine.Core.Abstracts;

namespace WebShine.Web.Api.Models.Dashboard {

	public class Field {
		public Int32 SortOrder { get; set; }
		public String Name { get; set; }
		public String Label { get; set; }
		public String Namespace { get; set; }
		public String Key { get; set; }
		public String Type { get; set; }
		public Boolean Editable { get; set; }
		public Boolean Required { get; set; }
		public Boolean Hidden { get; set; }
		public Dictionary<String, Object> CustomProperties { get; set; }
		public String DataType { get; set; }
		public List<Requirement> Requirements { get; set; }
	}
}