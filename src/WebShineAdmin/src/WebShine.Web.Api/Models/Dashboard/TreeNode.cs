using MongoDB.Bson;
using System;

namespace WebShine.Web.Api.Models.Dashboard {

	public class TreeNode {
		public String Name { get; set; }
		public ObjectId Id { get; set; }
		public ObjectId ParentId { get; set; }
		public Int32 SortOrder { get; set; }
		public String Template { get; set; }
		//public TreeNode[] Children { get; set; }
	}
}