using System;
using MongoDB.Bson;
using WebShine.Core.Abstracts;

namespace WebShine.Core.ValueProviders {

	public class EncryptedStringValueProvider : IValueProvider {

		public String Type {
			get {
				return SystemDataTypes.EncryptedString;
			}
		}

		public BsonValue GetDefaultValue() {
			return String.Empty;
		}

		public BsonValue GetValue(Object value) {
			return BsonValue.Create((String)value);
		}

		public Boolean CanFetch(BsonValue value) {
			return value.IsString;
		}

		public Object Fetch(BsonValue value) {
			// TODO: decrypt!!
			return value.AsString;
		}
	}
}
