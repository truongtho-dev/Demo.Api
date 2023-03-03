using Demo.Api.Extensions;
using Demo.Api.Securities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Demo.Api.Filters
{
	public class AccountPermissionsFilter : IAuthorizationFilter
	{
		private readonly Feature[] _features;

        public AccountPermissionsFilter(Feature[] features)
        {
            _features = features;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
		{
			var identityContext = context.HttpContext.GetIdentityContext();

			if(identityContext.AccountId == Guid.Empty || identityContext.Features == null || !identityContext.Features.Any())
			{
				context.Result = new ForbidResult();
				return;
			}

			var userFeatures = identityContext.Features;

			if(_features.Any(f => !userFeatures.Contains(f))) context.Result = new ForbidResult();
		}
	}
}
