using System;
using System.IO;

namespace WebShine.Core.Abstracts {

	public interface ITemplateParser {
		ITemplate Parse(Stream stream, ITemplateService service);
	}
}
