using System;
using System.Collections.Generic;

namespace WebShine.Web.Api.DataSources {
	
	public interface ISimpleDataSource {
		IEnumerable<KeyValuePair<Object, Object>> Get();
	}
}