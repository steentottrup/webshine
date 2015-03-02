using System;

namespace WebShine.Core.Abstracts {
	
	public interface IProperty {
		String Name { get; set; }
		//String Type { get; set; }
		Object Value { get; set; }
		//Boolean Editable { get; set; }
	}
}
