using System;

namespace WebShine.Core.Abstracts {

	public interface IContext {
		IElementTree ElementTree { get; }
		IElementRepository ElementRepository { get; }
		ITemplateService TemplateService { get; }
		ITemplateParser TemplateParser { get; }
		ICacheService CacheService { get; set; }
	}
}