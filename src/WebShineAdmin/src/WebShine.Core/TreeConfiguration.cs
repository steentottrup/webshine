using System;
using System.Collections.Generic;
using WebShine.Core.Abstracts;

namespace WebShine.Core {

	public class TreeConfiguration : ITreeConfiguration {
		public String Name { get; set; }
		public IEnumerable<String> RootTemplates { get; set; }
		public IEnumerable<TemplateRelation> AllowedTemplates { get; set; }
	}
}
