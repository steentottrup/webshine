using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WebShine.Web.Api.DataSources {

	public class CultureInfoDataSource : ISimpleDataSource {

		public IEnumerable<KeyValuePair<Object, Object>> Get() {
			return CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(c => new KeyValuePair<Object, Object>(c.Name, c.DisplayName)).OrderBy(c => c.Value);
		}
	}
}