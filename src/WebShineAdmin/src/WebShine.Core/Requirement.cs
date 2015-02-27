using System;
using WebShine.Core.Abstracts;

namespace WebShine.Core {

	public class Requirement : TextCarrier, IRequirement {
		public String Type { get; set; }
		public String Parameter { get; set; }
	}
}
