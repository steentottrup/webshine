using System;
using WebShine.Core.Abstracts;

namespace WebShine.Core {

	public class Property : IProperty {
		public String Name { get; set; }
		public Object Value { get; set; }
	}
}