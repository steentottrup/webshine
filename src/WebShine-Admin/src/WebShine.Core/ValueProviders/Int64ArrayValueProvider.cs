using System;
using System.Linq;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using WebShine.Core.Abstracts;

namespace WebShine.Core.ValueProviders {

	public class Int64ArrayValueProvider : IValueProvider {

		public String Type {
			get {
				return SystemDataTypes.Int64Array;
			}
		}

		public BsonValue GetDefaultValue() {
			return new BsonArray(new Int64[] { });
		}

		public Boolean CanFetch(BsonValue value) {
			return value.IsBsonArray && !value.AsBsonArray.Any(v => v.IsInt64 == false);
		}

		public Object Fetch(BsonValue value) {
			return value.AsBsonArray.Select(v => v.AsInt64).ToArray();
		}

		public BsonValue GetValue(Object value) {
			return new BsonArray(((JArray)value).Select(t => (Int64)t));
		}
	}
}
