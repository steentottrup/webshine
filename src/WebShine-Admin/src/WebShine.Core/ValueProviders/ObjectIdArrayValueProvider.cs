using System;
using System.Linq;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using WebShine.Core.Abstracts;

namespace WebShine.Core.ValueProviders {

	public class ObjectIdArrayValueProvider : IValueProvider {

		public String Type {
			get {
				return SystemDataTypes.ObjectIdArray;
			}
		}

		public BsonValue GetDefaultValue() {
			return new BsonArray(new ObjectId[] { });
		}

		public Boolean CanFetch(BsonValue value) {
			return value.IsBsonArray && !value.AsBsonArray.Any(v => v.IsObjectId == false);
		}

		public Object Fetch(BsonValue value) {
			return value.AsBsonArray.Select(v => v.AsObjectId).ToArray();
		}

		public BsonValue GetValue(Object value) {
			return new BsonArray(((JArray)value).Select(t => ObjectId.Parse((String)t)));
		}
	}
}
