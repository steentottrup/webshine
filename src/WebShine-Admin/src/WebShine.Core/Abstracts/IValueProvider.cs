using System;
using MongoDB.Bson;

namespace WebShine.Core.Abstracts {

	public interface IValueProvider {
		String Type { get; }
		BsonValue GetDefaultValue();
		BsonValue GetValue(Object value);
		//BsonArray GetDefaultArray();
		Boolean CanFetch(BsonValue value);
		Object Fetch(BsonValue value);
	}
}