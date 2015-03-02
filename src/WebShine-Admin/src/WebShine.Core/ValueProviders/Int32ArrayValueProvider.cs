using System;
using System.Linq;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using WebShine.Core.Abstracts;

namespace WebShine.Core.ValueProviders {

	public class Int32ArrayValueProvider : IValueProvider {

		public String Type {
			get {
				return SystemDataTypes.Int32Array;
			}
		}

		public BsonValue GetDefaultValue() {
			return new BsonArray(new Int32[] { });
		}

		public Boolean CanFetch(BsonValue value) {
			return value.IsBsonArray && !value.AsBsonArray.Any(v => v.IsInt32 == false);
		}

		public Object Fetch(BsonValue value) {
			return value.AsBsonArray.Select(v => v.AsInt32).ToArray();
		}

		public BsonValue GetValue(Object value) {
			return new BsonArray(((JArray)value).Select(t => (Int32)t));
		}
	}
}
