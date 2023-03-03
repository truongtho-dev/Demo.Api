using Demo.Api.Filters;
using System.Collections.Generic;
using System;

namespace Demo.Api.Securities
{
	public class IdentityContext
	{
		public string Email { get; set; }
		public Guid IdentityId { get; set; }
		public Guid AccountId { get; set; }
		public IEnumerable<Feature> Features { get; set; }
	}
}
