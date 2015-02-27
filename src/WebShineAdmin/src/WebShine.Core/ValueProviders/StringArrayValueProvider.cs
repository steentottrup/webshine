﻿using System;
using System.Linq;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using WebShine.Core.Abstracts;

namespace WebShine.Core.ValueProviders {

	public class StringArrayValueProvider : IValueProvider {

		public String Type {
			get {
				return SystemDataTypes.StringArray;
			}
		}

		public BsonValue GetDefaultValue() {
			return new BsonArray(new String[] { });
		}

		public Boolean CanFetch(BsonValue value) {
			return value.IsBsonArray && !value.AsBsonArray.Any(v => v.IsString == false);
		}

		public Object Fetch(BsonValue value) {
			return value.AsBsonArray.Select(v => v.AsString).ToArray();
		}

		public BsonValue GetValue(Object value) {
			return new BsonArray(((JArray)value).Select(t => (String)t));
		}
	}
}
