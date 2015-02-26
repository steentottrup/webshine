using System;
using WebShine.Core.Abstracts;

namespace WebShine.Web.Api.Dashboard {

	public class RepositoryHub : IRepositoryHub {

		public IElementRepository GetRepository(String type) {
			// TODO: Change, DI .. use type etc etc.
			return new ElementRepository(type);
		}
	}
}
