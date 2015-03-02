using System;
using System.Linq;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using WebShine.Core.Abstracts;

namespace WebShine.Core.ValueProviders {

	public class DateTimeArrayValueProvider : IValueProvider {

		public String Type {
			get {
				return SystemDataTypes.DateTimeArray;
			}
		}

		public BsonValue GetDefaultValue() {
			return new BsonArray(new DateTime[] { });
		}

		public Boolean CanFetch(BsonValue value) {
			return value.IsBsonArray && !value.AsBsonArray.Any(v => v.IsBsonDateTime == false);
		}

		public Object Fetch(BsonValue value) {
			return value.AsBsonArray.Select(v => v.ToUniversalTime()).ToArray();
		}

		public BsonValue GetValue(Object value) {
			return new BsonArray(((JArray)value).Select(t => (DateTime)t));
		}
	}
}