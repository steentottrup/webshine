using System;
using MongoDB.Bson;

namespace WebShine.Web.Api.Models.Dashboard {

	public class MoveData {
		public String NewParentId { get; set; }
		public Int32 NewIndex { get; set; }
	}
}