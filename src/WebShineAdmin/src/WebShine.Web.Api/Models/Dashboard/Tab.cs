using System;
using System.Collections.Generic;
using WebShine.Core.Abstracts;

namespace WebShine.Web.Api.Models.Dashboard {

	public class Tab {
		public String Name { get; set; }
		public String Label { get; set; }
		public List<Field> Fields { get; set; }
		public Int32 SortOrder { get; set; }
	}
}