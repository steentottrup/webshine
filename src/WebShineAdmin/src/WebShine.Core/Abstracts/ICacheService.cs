using System;

namespace WebShine.Core.Abstracts {

	public interface ICacheService {
		TEntity Get<TEntity>(String key) where TEntity : class;
		void Put(String key, Object data);
	}
}