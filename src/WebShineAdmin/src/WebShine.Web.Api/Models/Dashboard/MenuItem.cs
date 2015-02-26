using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebShine.Web.Api.Models.Dashboard {
	
	public class MenuItem {
		public String Namespace { get; set; }
		public String Key { get; set; }
		public String Title { get; set; }
		public String Icon { get; set; }
		public String Link { get; set; }
		public MenuItem[] Items { get; set; }
	}
}
