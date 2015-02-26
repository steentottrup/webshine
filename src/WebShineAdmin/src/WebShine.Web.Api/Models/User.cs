using MongoDB.Bson;
using System;

namespace WebShine.Web.Api.Models {

	public class User {
		public ObjectId Id { get; set; }
		public Boolean Authenticated { get; set; }
		public String Name { get; set; }
		public String Language { get; set; }
	}
}
