using System;
using System.ComponentModel.DataAnnotations;

namespace WebShine.Web.Api.Models {

	public class LogOn {
		[Required]
		public String Username { get; set; }
		[Required]
		public String Password { get; set; }
		public Boolean RememberMe { get; set; }
	}
}
