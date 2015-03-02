using System;
using MongoDB.Bson;
using WebShine.Core.Abstracts;

namespace WebShine.Core.ValueProviders {

	public class Int32ValueProvider : IValueProvider {

		public String Type {
			get {
				return SystemDataTypes.Int32;
			}
		}

		public BsonValue GetDefaultValue() {
			return default(Int32?);
		}

		public Boolean CanFetch(BsonValue value) {
			return value.IsInt32;
		}

		public Object Fetch(BsonValue value) {
			return value.AsInt32;
		}

		public BsonValue GetValue(Object value) {
			return BsonValue.Create((Int32)value);
		}
	}
}