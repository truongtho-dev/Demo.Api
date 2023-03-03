using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Demo.Api.Securities
{
	public class UserContextBuilder : IIdentityContextBuilder
	{
		public IdentityContext BuildAsync(HttpContext context)
		{
			// in microservices, need to call identity service, then get some identityContext from the Claim of context
			var identityContext = new IdentityContext
			{
				Email = "teo@gmail.com",
				AccountId = new Guid("2c15a1ac-ad89-4692-9ea5-bf70fcf80baa"),
				IdentityId = new Guid("42d99bf9-0fab-48e0-9f57-87939d59374a"),
				Features = new Feature[] {Feature.Feature1, Feature.Feature2}
			};

			return identityContext;
		}
	}
}
