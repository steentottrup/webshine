using System;

namespace WebShine.Core.Abstracts {

	public abstract class BaseTemplateService : ITemplateService {
		protected readonly ICacheService cache;

		protected BaseTemplateService(ICacheService cache) {
			this.cache = cache;
		}

		public abstract Boolean Exists(String name);
		public abstract ITemplate Get(String name);

		protected virtual ITemplate GetFromCache(String name) {
			return this.cache.Get<ITemplate>(this.GetCacheKey(name));
		}

		protected virtual void Cache(String name, ITemplate template) {
			this.cache.Put(this.GetCacheKey(name), template);
		}

		protected virtual String GetCacheKey(String name) {
			return String.Format("Template:{0}", name);
		}
	}
}