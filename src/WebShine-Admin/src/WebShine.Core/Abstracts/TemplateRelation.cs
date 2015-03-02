using System;
using System.Collections.Generic;

namespace WebShine.Core.Abstracts {
	
	public class TemplateRelation {
		public String Template { get; set; }
		public IEnumerable<String> ChildTemplates { get; set; }
	}
}
