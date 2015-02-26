using System;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using MongoDB.Bson;

namespace WebShine.Web.Api.Converters {

	public class ObjectIdModelBinder : IModelBinder {

		public Boolean BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext) {
			if (bindingContext.ModelType != typeof(ObjectId)) {
				return false;
			}

			ValueProviderResult val = bindingContext.ValueProvider.GetValue(
				bindingContext.ModelName);
			if (val == null) {
				return false;
			}

			String key = val.RawValue as String;
			if (key == null) {
				bindingContext.ModelState.AddModelError(
					bindingContext.ModelName, "Wrong value type");
				return false;
			}

			ObjectId id;
			if (ObjectId.TryParse(key, out id)) {
				bindingContext.Model = id;
				return true;
			}

			bindingContext.ModelState.AddModelError(
				bindingContext.ModelName, "Cannot convert value to Location");
			return false;
		}
	}
}
