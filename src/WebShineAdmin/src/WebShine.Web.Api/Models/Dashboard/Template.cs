using System;
using System.Collections.Generic;
using WebShine.Core.Abstracts;

namespace WebShine.Web.Api.Models.Dashboard {

	public class Template {
		public String Name { get; set; }
		public List<Tab> Tabs { get; set; }
	}
}