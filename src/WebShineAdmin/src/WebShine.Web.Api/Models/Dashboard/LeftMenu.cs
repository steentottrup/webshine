using System;

namespace WebShine.Web.Api.Models.Dashboard {

	public class LeftMenu {
		public String Namespace { get; set; }
		public String Key { get; set; }
		public String Icon { get; set; }
		public MenuItem[] Items { get; set; }
	}
}