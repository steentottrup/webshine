using System;
using Newtonsoft.Json.Serialization;

namespace WebShine.Web.Api {

	// TODO: Needed at all ????
	public class LowercaseContractResolver : DefaultContractResolver {

		protected override String ResolvePropertyName(String propertyName) {
			//for (Int32 index = 0; index < propertyName.Length; index++) {
			//	if (Char.IsUpper(propertyName[index])) {

			//	}
			//}
			return propertyName.ToLower();
		}
	}
}
