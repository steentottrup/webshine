using System;
using MongoDB.Bson;
using WebShine.Core.Abstracts;

namespace WebShine.Core.ValueProviders {

	public class ObjectIdValueProvider : IValueProvider {

		public String Type {
			get {
				return SystemDataTypes.ObjectId;
			}
		}

		public BsonValue GetDefaultValue() {
			return ObjectId.Empty;
		}

		public Boolean CanFetch(BsonValue value) {
			return value.IsObjectId;
		}

		public Object Fetch(BsonValue value) {
			return value.AsObjectId;
		}

		public BsonValue GetValue(Object value) {
			// TODO: Parse??
			return BsonValue.Create((ObjectId)value);
		}
	}
}
