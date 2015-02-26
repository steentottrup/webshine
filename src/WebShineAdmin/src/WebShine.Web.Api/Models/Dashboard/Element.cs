using System;
using MongoDB.Bson;
using WebShine.Core.Abstracts;

namespace WebShine.Web.Api.Models.Dashboard {

	public class Element {
		public ObjectId Id { get; set; }
		public ObjectId ParentId { get; set; }
		public String Name { get; set; }
		public String Tree { get; set; }
		public String Template { get; set; }
		public Property[] Properties { get; set; }
	}
}