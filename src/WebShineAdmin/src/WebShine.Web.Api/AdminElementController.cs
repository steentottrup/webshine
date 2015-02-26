using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using MongoDB.Bson;
using WebShine.Core.Abstracts;
using WebShine.Web.Api.Dashboard;
using WebShine.Web.Api.Helpers;
using WebShine.Web.Api.Models.Dashboard;

namespace WebShine.Web.Api {

	[RoutePrefix("admin/api/element/{type}")]
	public class AdminElementController : BaseAuthenticatedApiController {
		protected readonly IRepositoryHub hub;

		// TODO: Remove!!
		public AdminElementController()
			: this(new RepositoryHub()) {
		}

		public AdminElementController(IRepositoryHub hub)
			: base() {

			this.hub = hub;
		}

		[Route("{id}")]
		[HttpPost]
		public HttpResponseMessage Create(String type, [ModelBinder]ObjectId id, [FromBody]NewElement element) {
			IElement newElement = this.hub.GetRepository(type).Create(element.Template, element.Name, id);
			return this.Request.CreateResponse<Element>(HttpStatusCode.OK, ElementHelper.Populate( newElement));
		}

		[Route("{id}")]
		[HttpGet]
		public HttpResponseMessage Read(String type, [ModelBinder]ObjectId id) {
			IElement newElement = this.hub.GetRepository(type).Read(id);
			if (newElement == null) {
				return this.CreateNotFoundResponse(type);
			}
			return this.Request.CreateResponse<Element>(HttpStatusCode.OK, ElementHelper.Populate(this.hub.GetRepository(type).Read(id)));
		}

		[Route("{id}")]
		[HttpPut]
		public HttpResponseMessage Update(String type, [ModelBinder]ObjectId id, [FromBody]IDictionary<String, Object> values) {
			// TODO: Server-side validation
			IElement element = this.hub.GetRepository(type).Update(id, values);
			return this.Request.CreateResponse<Element>(HttpStatusCode.OK, ElementHelper.Populate(element));
		}

		[Route("{id}")]
		[HttpDelete]
		public HttpResponseMessage Delete(String type, [ModelBinder]ObjectId id) {
			this.hub.GetRepository(type).Delete(id);
			return this.Request.CreateResponse<String>(HttpStatusCode.OK, "ok");
		}
	}
}
