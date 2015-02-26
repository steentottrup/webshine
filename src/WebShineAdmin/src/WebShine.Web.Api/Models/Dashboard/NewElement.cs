using System;
using System.Web.Http.ModelBinding;
using MongoDB.Bson;

namespace WebShine.Web.Api.Models.Dashboard {

	public class NewElement {
		public String Name { get; set; }
		public String Template { get; set; }
	}
}
