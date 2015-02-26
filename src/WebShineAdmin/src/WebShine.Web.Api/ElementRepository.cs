using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using WebShine.Core;
using WebShine.Core.Abstracts;
using WebShine.Core.Data;
using WebShine.Core.ValueProviders;
using WebShine.Web.Api.Dashboard;
using WebShine.Web.Api.Helpers;
using MongoDB.Driver;
using WebShine.Core.LocalXmlServices;
using Newtonsoft.Json.Linq;

namespace WebShine.Web.Api {

	public class ElementRepository : IElementRepository {
		protected readonly ITemplateService service;
		protected readonly MongoConnection conn;
		protected readonly String type;
		private readonly List<IValueProvider> providers = new List<IValueProvider>();

		public ElementRepository(String type)
			: this(type, new LocalFileSystemTemplateService(new XmlTemplateParser(), new MemoryCacheService())) {
		}

		public ElementRepository(String type, ITemplateService service) {
			this.service = service;
			this.type = type;

			// TODO: Get from somewhere!!
			this.conn = MongoConnection.Create();

			// TODO: Get from somewhere!!
			this.providers.Add(new StringValueProvider());
			this.providers.Add(new Int32ValueProvider());
			this.providers.Add(new Int64ValueProvider());
			this.providers.Add(new DateTimeValueProvider());
			this.providers.Add(new ObjectIdValueProvider());

			this.providers.Add(new StringArrayValueProvider());
			this.providers.Add(new Int32ArrayValueProvider());
			this.providers.Add(new Int64ArrayValueProvider());
			this.providers.Add(new DateTimeArrayValueProvider());
			this.providers.Add(new ObjectIdArrayValueProvider());

			this.providers.Add(new EncryptedStringValueProvider());
		}

		#region CRUDs
		public IElement Create(String template, String name, ObjectId parentId) {
			ITemplate temp = this.service.Get(template);

			if (parentId != ObjectId.Empty) {
				DBElement parent = this.GetCollection().FindOneByIdAs<DBElement>(parentId);
				if (parent == null) {
					throw new ApplicationException("unknown parent");
				}
			}
			IEnumerable<DBElement> children = this.GetCollection().FindAs<DBElement>(Query.EQ(DBElement.FieldNames.ParentId, parentId));

			// TODO: Validate!!!

			Core.Data.DBElement e = new Core.Data.DBElement {
				Name = name,
				ParentId = parentId,
				Template = template,
				Properties = new BsonDocument(false)
			};

			foreach (IField field in temp.Tabs.SelectMany(t => t.Fields)) {
				if (this.ValidSystemNameField(field)) {
					// Do nothing, name is already stored on the "root" level of the element
				}
				// Is this one of the datestamp system fields ??
				else if ((field.Name == SystemFieldNames.Created && field.IsValidSystemCreatedField()) ||
					(field.Name == SystemFieldNames.Changed && field.IsValidSystemChangedField())) {

					e.Properties.Add(new BsonElement(field.Name, DateTime.UtcNow));
				}
				// Is this one of the author system fields ??
				else if ((field.Name == SystemFieldNames.Author && field.IsValidSystemAuthorField()) ||
					(field.Name == SystemFieldNames.Editor && field.IsValidSystemEditorField())) {

					// TODO: Get user id!!
					e.Properties.Add(new BsonElement(field.Name, ObjectId.GenerateNewId()));
				}
				else if ((field.Name == SystemFieldNames.SortOrder && field.IsValidSystemSortOrderField())) {
					Int32 maxSortOrder = 1;
					if (children.Any()) {
						maxSortOrder = children.SelectMany(c => c.Properties).Where(p => p.Name == SystemFieldNames.SortOrder && p.Value.AsNullableInt32.HasValue).Max(p => p.Value.AsNullableInt32.Value) + 1;
					}
					e.Properties.Add(new BsonElement(field.Name, maxSortOrder));
				}
				// Nope, a regular custom field!
				else {
					IValueProvider vp = this.GetValueProvider(field);
					e.Properties.Add(new BsonElement(field.Name, vp.GetDefaultValue()));
				}
			}

			this.GetCollection().Insert(e);

			return e.ToElement(type);
		}

		public IElement Read(ObjectId id) {
			Core.Data.DBElement e = this.GetCollection().FindOneByIdAs<Core.Data.DBElement>(id);
			if (e != null) {
				ITemplate temp = this.service.Get(e.Template);
				return Populate(e, temp);
			}
			return null;
		}

		public IElement Update(ObjectId id, IDictionary<String, Object> values) {
			DBElement element = this.GetCollection().FindOneByIdAs<DBElement>(id);
			if (element != null) {
				// TODO: Validation!!
				ITemplate template = this.service.Get(element.Template);

				foreach (IField field in template.Tabs.SelectMany(t => t.Fields)) {
					IValueProvider vp = this.GetValueProvider(field);

					if (this.ValidSystemNameField(field)) {
						element.Name = (String)values[field.Name];
					}
					else if (field.Editable) {
						if (values.ContainsKey(field.Name)) {
							this.UpdateField(element, vp.GetValue(values[field.Name]), field.Name);
						}
						else {
							this.UpdateField(element, this.AddDefaultValue(field).Value, field.Name);
						}
					}
					else {
						// Handle system fields!
						if (field.Name == SystemFieldNames.Changed && field.IsValidSystemChangedField()) {
							this.UpdateField(element, BsonValue.Create(DateTime.UtcNow), field.Name);
						}
						else if (field.Name == SystemFieldNames.Editor && field.IsValidSystemEditorField()) {
							// TODO: Get user id!!!
							this.UpdateField(element, BsonValue.Create(ObjectId.GenerateNewId()), field.Name);
						}
					}
				}

				this.GetCollection().Save(element);

				return new Element {
					Id = element.Id,
					Name = element.Name,
					Template = element.Template,
					Tree = type,
					Properties = element.Properties.Select(x => new Property { Name = x.Name, Value = BsonTypeMapper.MapToDotNetValue(x.Value) }).ToArray()
				};
			}

			throw new ApplicationException("not found");
			//TODO: Else ?!!??!
		}

		public void Delete(ObjectId id) {
			DBElement element = this.GetCollection().FindOneByIdAs<DBElement>(id);
			ObjectId parentId = element.ParentId;
			if (element == null) {
				//TODO: Else ?!!??!
				throw new ApplicationException("not found");
			}
			// Let's remove the element and descendats!
			this.RecursiveDelete(element);
			// Let's resort the siblings!
			this.ResortChildren(parentId);
		}
		#endregion

		#region Tree methods
		public IEnumerable<IElement> ReadTreeLevel(ObjectId parentId) {
			return this.GetCollection()
				.FindAs<Core.Data.DBElement>(Query.EQ(Core.Data.DBElement.FieldNames.ParentId, parentId))
				.Select(e => new Element { Id = e.Id, Template = e.Template, Name = e.Name, Properties = e.Properties.Select(p => new Property { Name = p.Name, Value = BsonTypeMapper.MapToDotNetValue(p.Value) }).ToArray() })
				.ToList();
		}

		public void MoveNode(ObjectId id, Int32 newIndex) {
			DBElement element = this.GetElement(id);
			if (element == null) {
				// TODO:
				throw new ApplicationException("element not found");
			}
			// TODO: 
			this.MoveNode(id, element.ParentId, newIndex);
		}

		public void MoveNode(ObjectId id, ObjectId newParentId, Int32 newIndex) {
			DBElement element = this.GetElement(id);
			if (element == null) {
				// TODO:
				throw new ApplicationException("element not found");
			}
			this.MoveNode(element, newParentId, newIndex);
		}
		#endregion

		private MongoCollection GetCollection() {
			return this.conn.GetCollection(this.type);
		}

		private IValueProvider GetValueProvider(IField field) {
			IValueProvider vp = this.providers.FirstOrDefault(v => v.Type == field.DataType);
			if (vp == null) {
				// TODO: Handle with some default VP ?? String ??
				throw new ApplicationException("unhandled value type, " + field.DataType);
			}
			return vp;
		}

		private BsonElement AddDefaultValue(IField field) {
			IValueProvider vp = this.GetValueProvider(field);
			return new BsonElement(field.Name, vp.GetDefaultValue());
		}

		private Boolean ValidSystemNameField(IField field) {
			return field.Name == SystemFieldNames.Name &&
				field.DataType == SystemDataTypes.String &&
				field.Required == true;
		}

		private IElement Populate(DBElement element, ITemplate template) {
			BsonElement nameProperty = element.Properties.FirstOrDefault(p => p.Name == SystemFieldNames.Name);
			if (nameProperty == null) {
				nameProperty = new BsonElement(SystemFieldNames.Name, new StringValueProvider().GetDefaultValue());
				element.Properties.Add(nameProperty);
			}
			nameProperty.Value = element.Name;
			return new Element {
				Template = element.Template,
				Id = element.Id,
				ParentId = element.ParentId,
				Name = element.Name,
				Tree = type,
				Properties = element.Properties.Select(p => this.GetProperty(p, template)).ToArray()
			};
		}

		private IProperty GetProperty(BsonElement element, ITemplate template) {
			Property output = new Property {
				Name = element.Name
			};
			IField field = template.Tabs.SelectMany(t => t.Fields).First(f => f.Name == element.Name);
			if (field != null) {
				output.Value = this.GetValue(element, field);
			}
			return output;
		}

		private Object GetValue(BsonElement element, IField field) {
			IValueProvider vp = this.GetValueProvider(field);
			if (!vp.CanFetch(element.Value)) {
				throw new ApplicationException("can't fetch.. " + field.DataType);
			}
			return vp.Fetch(element.Value);
			//if (field.DataType == typeof(String) && element.Value.IsString) {
			//	output = element.Value.AsString;
			//}
			//else if (field.DataType == typeof(Int32?) && element.Value.IsInt32) {
			//	output = element.Value.AsNullableInt32;
			//}
			//else if (field.DataType == typeof(Int64?) && element.Value.IsInt64) {
			//	output = element.Value.AsNullableInt64;
			//}
			//else if (field.DataType == typeof(DateTime?) && element.Value.IsValidDateTime) {
			//	output = element.Value.ToNullableUniversalTime();
			//}
			//else if (field.DataType == typeof(ObjectId) && element.Value.IsObjectId) {
			//	output = element.Value.AsNullableObjectId;
			//}
			//else if (field.DataType == typeof(String[]) && element.Value.IsBsonArray) {
			//	output = element.Value.AsBsonArray.Select(x => x.AsString).ToArray();
			//}
			//else if (field.DataType == typeof(Int32?[]) && element.Value.IsBsonArray) {
			//	output = element.Value.AsBsonArray.Select(x => x.AsNullableInt32).ToArray();
			//}
			//else if (field.DataType == typeof(Int64?[]) && element.Value.IsBsonArray) {
			//	output = element.Value.AsBsonArray.Select(x => x.AsNullableInt64).ToArray();
			//}
			//else if (field.DataType == typeof(DateTime?[]) && element.Value.IsBsonArray) {
			//	output = element.Value.AsBsonArray.Select(x => x.AsNullableDateTime).ToArray();
			//}
			//else if (field.DataType == typeof(ObjectId[]) && element.Value.IsBsonArray) {
			//	output = element.Value.AsBsonArray.Select(x => x.AsNullableObjectId).ToArray();
			//}
			//else {
			//	throw new ApplicationException("unknown " + field.DataType);
			//}
		}

		private void UpdateField(DBElement element, BsonValue value, String fieldName) {
			if (element.Properties.Contains(fieldName)) {
				element.Properties[fieldName] = value;
			}
			else {
				element.Properties.Add(new BsonElement(fieldName, value));
			}
		}

		private void RecursiveDelete(DBElement parent) {
			IEnumerable<DBElement> children = this.GetCollection().FindAs<DBElement>(Query.EQ(DBElement.FieldNames.ParentId, parent.Id));
			foreach (DBElement child in children) {
				this.RecursiveDelete(child);
			}

			this.GetCollection().Remove(Query.EQ(DBElement.FieldNames.Id, parent.Id));
		}

		private DBElement GetElement(ObjectId id) {
			return this.GetCollection().FindOneByIdAs<DBElement>(id);
		}

		private void ResortChildren(DBElement movedChild, Int32 newIndex, ObjectId parentId) {
			MongoCollection coll = this.GetCollection();
			// Let's get the children!
			List<DBElement> children = coll.FindAs<DBElement>(Query.And(Query.NE(DBElement.FieldNames.Id, movedChild.Id), Query.EQ(DBElement.FieldNames.ParentId, parentId))).ToList();

			Dictionary<String, ITemplate> templates = new Dictionary<String, ITemplate>();
			templates.Add(movedChild.Template, this.service.Get(movedChild.Template));
			List<DBElement> sortables = new List<DBElement>();
			foreach (DBElement e in children) {
				if (!templates.ContainsKey(e.Template)) {
					templates.Add(e.Template, this.service.Get(e.Template));
				}
				if (templates[e.Template].HasSortOrderField()) {
					sortables.Add(e);
				}
			}

			if ((sortables.Count + 1) < newIndex) {
				newIndex = sortables.Count;
			}

			sortables = sortables.OrderBy(e => e.SortOrder(templates[e.Template])).ToList();

			Int32 index = 1;
			foreach (DBElement e in sortables) {
				if (index == newIndex) {
					movedChild.SetSortOrder(templates[movedChild.Template], index);
					index++;
				}
				e.SetSortOrder(templates[e.Template], index);
				index++;
			}

			coll.Save(movedChild);
			foreach (DBElement sibling in children) {
				coll.Save(sibling);
			}
		}

		private void ResortChildren(ObjectId parentId) {
			MongoCollection coll = this.GetCollection();
			// Let's get the children!
			List<DBElement> children = coll.FindAs<DBElement>(Query.EQ(DBElement.FieldNames.ParentId, parentId)).ToList();

			Dictionary<String, ITemplate> templates = new Dictionary<String, ITemplate>();
			List<DBElement> sortables = new List<DBElement>();
			foreach (DBElement e in children) {
				if (!templates.ContainsKey(e.Template)) {
					templates.Add(e.Template, this.service.Get(e.Template));
				}
				if (templates[e.Template].HasSortOrderField()) {
					sortables.Add(e);
				}
			}

			sortables = sortables.OrderBy(e => e.SortOrder(templates[e.Template])).ToList();

			Int32 index = 1;
			foreach (DBElement e in sortables) {
				e.SetSortOrder(templates[e.Template], index);
				index++;
			}

			foreach (DBElement sibling in children) {
				coll.Save(sibling);
			}
		}

		private void MoveNode(DBElement element, ObjectId newParentId, Int32 newIndex) {
			MongoCollection coll = this.GetCollection();
			ObjectId oldParentId = element.ParentId;

			// Same parent??
			if (oldParentId == newParentId) {
				// Yes, let's just re-sort!

				// On root? Or?
				if (oldParentId != ObjectId.Empty) {
					// Not root!! Let's make sure the parent exists!
					DBElement parent = coll.FindOneByIdAs<DBElement>(element.ParentId);
					// Did we get a parent?
					if (parent == null) {
						// TODO:
						throw new ApplicationException("parent not found");
					}
				}

				// Let's just resort the children then!
				this.ResortChildren(element, newIndex, oldParentId);
			}
			else {
				// Nope, move and re-sort on source and target!
				if (element.ParentId != ObjectId.Empty) {
					DBElement oldParent = coll.FindOneByIdAs<DBElement>(element.ParentId);
					if (oldParent == null) {
						// TODO:
						throw new ApplicationException("old parent not found");
					}
				}
				if (newParentId != ObjectId.Empty) {
					DBElement newParent = coll.FindOneByIdAs<DBElement>(newParentId);
					if (newParent == null) {
						// TODO:
						throw new ApplicationException("new parent not found");
					}
				}
				element.ParentId = newParentId;
				// Let's resort the children on the new parent!
				this.ResortChildren(element, newIndex, newParentId);
				// Let's resort the children on the old parent!
				this.ResortChildren(oldParentId);
			}
		}
	}
}
