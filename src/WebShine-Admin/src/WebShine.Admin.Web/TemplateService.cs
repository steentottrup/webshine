using System;
using WebShine.Core.Abstracts;
using WebShine.Core.Abstracts.Services;

namespace WebShine.Admin.Web {

	public class TemplateService : BaseTemplateService {
		protected readonly ITemplateCollection templateCollection;
		protected readonly ITemplateParser parser;

		public TemplateService(ITemplateCollection templateCollection, ITemplateParser parser, ICacheService cache) : base(cache) {
			this.templateCollection = templateCollection;
			this.parser = parser;
		}

		public override Boolean Exists(String name) {
			return this.templateCollection.Exists(name);
		}

		public override ITemplate Get(String name) {
			ITemplate output = this.GetFromCache(name);
			if (output == null ) {
				if (!this.Exists(name)) {
					// TODO:
					throw new ApplicationException("none existing template " + name);
				}

				output = this.parser.Parse(this.templateCollection.Get(name), this);
				this.Cache(name, output);
			}

			return output;
		}
	}
}