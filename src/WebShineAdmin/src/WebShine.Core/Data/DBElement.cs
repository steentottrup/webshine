using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebShine.Core.Data {

	public class DBElement {
		public ObjectId Id { get; set; }
		[BsonElement(FieldNames.ParentId)]
		public ObjectId ParentId { get; set; }
		[BsonElement(FieldNames.Name)]
		public String Name { get; set; }
		[BsonElement(FieldNames.Template)]
		public String Template { get; set; }
		[BsonElement(FieldNames.Properties)]
		public BsonDocument Properties { get; set; }

		public static class FieldNames {
			public const String Id = "_id";
			public const String ParentId = "pid";
			public const String Name = "name";
			public const String Template = "template";
			public const String Properties = "props";
		}
	}
}
