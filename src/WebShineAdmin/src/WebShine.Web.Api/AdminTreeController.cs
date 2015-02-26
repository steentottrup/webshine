using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using MongoDB.Bson;
using WebShine.Core.Abstracts;
using WebShine.Core;
using WebShine.Core.LocalXmlServices;
using WebShine.Web.Api.Dashboard;
using WebShine.Web.Api.Models.Dashboard;

namespace WebShine.Web.Api {

	[RoutePrefix("admin/api/tree/{type}")]
	public class AdminTreeController : BaseAuthenticatedApiController {
		protected readonly IRepositoryHub hub;
		protected readonly ITreeConfigurationService service;
		protected readonly ITemplateService templateService;

		// TODO: Remove!!
		public AdminTreeController()
			: this(new RepositoryHub(), new LocalFileSystemTemplateService(new XmlTemplateParser(), new MemoryCacheService())) {
		}

		public AdminTreeController(IRepositoryHub hub, ITemplateService service)
			: base() {

			this.hub = hub;
			this.service = new LocalFileSystemTreeConfigurationService(new XmlTreeConfigurationParser());
			this.templateService = service;
		}

		[Route("templates")]
		[HttpGet]
		public HttpResponseMessage GetTemplates(String type) {
			if (this.service.Exists(type)) {
				ITreeConfiguration config = this.service.Get(type);
				return this.Request.CreateResponse(HttpStatusCode.OK, config);
			}
			else {
				return this.Request.CreateResponse(HttpStatusCode.NotFound);
			}
		}

		[Route("")]
		[HttpGet]
		public HttpResponseMessage Get(String type) {
			return this.Request.CreateResponse<TreeNode[]>(new TreeNode[] { new TreeNode { Name = "Root", Id = ObjectId.Empty, ParentId = ObjectId.Empty } });
		}

		[Route("{id}")]
		[HttpGet]
		public HttpResponseMessage Get(String type, [ModelBinder]ObjectId id) {
			IEnumerable<IElement> nodes = this.hub.GetRepository(type).ReadTreeLevel(id);
			List<TreeNode> output = new List<TreeNode>();
			foreach (IElement element in nodes) {
				TreeNode node = new TreeNode {
					Id = element.Id,
					ParentId = id,
					Name = element.Name,
					Template = element.Template
				};
				ITemplate template = this.templateService.Get(element.Template);
				if (template.HasSortOrderField()) {
					node.SortOrder = -1;
					IProperty sortOrderProperty = element.Properties.FirstOrDefault(p => p.Name == SystemFieldNames.SortOrder);
					if (sortOrderProperty != null) {
						Int32? sortOrder = sortOrderProperty.Value as Int32?;
						if (sortOrder.HasValue) {
							node.SortOrder = sortOrder.Value;
						}
					}
				}
				output.Add(node);
			}

			return this.Request.CreateResponse<TreeNode[]>(output.ToArray());
		}

		[Route("{id}")]
		[HttpPost]
		public HttpResponseMessage Post(String type, [ModelBinder]ObjectId id, [FromBody]MoveData data) {
			ObjectId newParentId;
			if (ObjectId.TryParse(data.NewParentId, out newParentId)) {
				IElement element = this.hub.GetRepository(type).Read(id);
				if (element.ParentId != newParentId) {
					this.hub.GetRepository(type).MoveNode(id, newParentId, data.NewIndex);
				}
				else {
					this.hub.GetRepository(type).MoveNode(id, data.NewIndex);
				}
				return this.Request.CreateResponse<String>(HttpStatusCode.OK, "ok");
			}
			return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
		}

		//[Route("{id}/{newindex}")]
		//[HttpPost]
		//public HttpResponseMessage Post(String type, [ModelBinder]ObjectId id, Int32 newIndex) {
		//	// TODO:
		//	return this.Request.CreateResponse<String>(HttpStatusCode.OK, "ok");
		//}
	}
}
