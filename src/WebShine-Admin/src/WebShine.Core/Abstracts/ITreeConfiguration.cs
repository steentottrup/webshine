using System;
using System.Collections.Generic;

namespace WebShine.Core.Abstracts {
	
	public interface ITreeConfiguration {
		String Name { get; set; }
		IEnumerable<String> RootTemplates { get; set; }
		IEnumerable<TemplateRelation> AllowedTemplates { get; set; }
	}
}
