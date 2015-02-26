using System;
using System.Collections.Generic;
using System.Linq;

namespace WebShine.Web.Api.DataSources {

	public class TimezoneDataSource : ISimpleDataSource {

		public IEnumerable<KeyValuePair<Object, Object>> Get() {
			return TimeZoneInfo.GetSystemTimeZones().Select(tz => new KeyValuePair<Object, Object>(tz.Id, tz.DisplayName));
		}
	}
}
