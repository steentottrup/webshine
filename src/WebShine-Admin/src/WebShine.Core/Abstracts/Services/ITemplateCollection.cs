using System;
using System.IO;

namespace WebShine.Core.Abstracts.Services {

	public interface ITemplateCollection {
		Stream Get(String name);
		String[] GetAll();
		Boolean Exists(String name);
	}
}