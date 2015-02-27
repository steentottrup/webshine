using System;
using MongoDB.Bson;
using WebShine.Core.Abstracts;

namespace WebShine.Core.ValueProviders {

	public class Int64ValueProvider : IValueProvider {

		public String Type {
			get {
				return SystemDataTypes.Int64;
			}
		}

		public BsonValue GetDefaultValue() {
			return default(Int64?);
		}

		public Boolean CanFetch(BsonValue value) {
			return value.IsInt64;
		}

		public Object Fetch(BsonValue value) {
			return value.AsInt64;
		}

		public BsonValue GetValue(Object value) {
			return BsonValue.Create((Int64)value);
		}
	}
}
