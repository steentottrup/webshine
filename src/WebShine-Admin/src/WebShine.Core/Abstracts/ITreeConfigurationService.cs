using System;

namespace WebShine.Core.Abstracts {

	public interface ITreeConfigurationService {
		Boolean Exists(String name);
		ITreeConfiguration Get(String name);
	}
}
