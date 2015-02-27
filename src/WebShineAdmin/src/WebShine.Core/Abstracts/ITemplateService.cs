using System;

namespace WebShine.Core.Abstracts {

	public interface ITemplateService {
		Boolean Exists(String name);
		ITemplate Get(String name);
	}
}