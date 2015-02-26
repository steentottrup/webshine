using System;

namespace WebShine.Core.Abstracts.Services {

	public interface ITemplateCollection {
		String Get(String name);
		String[] GetAll();
	}
}