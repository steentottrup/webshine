using System;

namespace WebShine.Core.Abstracts {

	public interface IRepositoryHub {
		IElementRepository GetRepository(String type);
	}
}