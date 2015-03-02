using System;
using WebShine.Core.Abstracts;

namespace WebShine.Core {

	public class Property : IProperty {
		public String Name { get; set; }
		//public String Type { get; set; }
		public Object Value { get; set; }
		//public Boolean Editable { get; set; }
	}
}