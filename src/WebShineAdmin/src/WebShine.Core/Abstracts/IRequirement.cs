using System;

namespace WebShine.Core.Abstracts {

	public interface IRequirement : ITextCarrier {
		String Type { get; set; }
		String Parameter { get; set; }
	}
}
