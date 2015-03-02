using System;
using MongoDB.Bson;
using WebShine.Core.Abstracts;

namespace WebShine.Core.ValueProviders {

	public class DateTimeValueProvider : IValueProvider {

		public String Type {
			get {
				return SystemDataTypes.DateTime;
			}
		}

		public BsonValue GetDefaultValue() {
			return default(DateTime?);
		}

		public Boolean CanFetch(BsonValue value) {
			return value.IsValidDateTime;
		}

		public Object Fetch(BsonValue value) {
			return value.ToUniversalTime();
		}

		public BsonValue GetValue(Object value) {
			return BsonValue.Create((DateTime)value);
		}
	}
}
