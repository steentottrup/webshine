using System;
using System.Collections.Generic;
using WebShine.Core.Abstracts;

namespace WebShine.Core.LocalXmlServices {

	public class MemoryCacheService : ICacheService {
		private static Dictionary<String, Object> cache = new Dictionary<String, Object>();
		private static Object cacheLock = new Object();

		public TEntity Get<TEntity>(String key) where TEntity : class {
			if (cache.ContainsKey(key)) {
				return cache[key] as TEntity;
			}

			return null;
		}

		public void Put(String key, Object data) {
			lock (cacheLock) {
				if (!cache.ContainsKey(key)) {
					cache.Add(key, data);
				}
				else {
					cache[key] = data;
				}
			}
		}
	}
}

