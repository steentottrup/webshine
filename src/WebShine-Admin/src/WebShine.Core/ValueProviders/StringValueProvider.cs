using System;
using MongoDB.Bson;
using WebShine.Core.Abstracts;

namespace WebShine.Core.ValueProviders {

	public class StringValueProvider : IValueProvider {

		public String Type {
			get {
				return SystemDataTypes.String;
			}
		}

		public BsonValue GetDefaultValue() {
			return String.Empty;
		}

		public Boolean CanFetch(BsonValue value) {
			return value.IsString;
		}

		public Object Fetch(BsonValue value) {
			return value.AsString;
		}

		public BsonValue GetValue(Object value) {
			return BsonValue.Create((String)value);
		}
	}
}