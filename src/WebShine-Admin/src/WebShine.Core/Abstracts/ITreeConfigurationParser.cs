using System;
using System.IO;

namespace WebShine.Core.Abstracts {

	public interface ITreeConfigurationParser {
		ITreeConfiguration Parse(Stream stream, ITreeConfigurationService service);
	}
}