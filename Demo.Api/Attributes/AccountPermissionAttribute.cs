using Demo.Api.Filters;
using Demo.Api.Securities;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Attributes
{
	public class AccountPermissionAttribute: TypeFilterAttribute
	{
        public Feature[] Features { get; private set; }

        public AccountPermissionAttribute(params Feature[] features): base(typeof(AccountPermissionsFilter))
        {
            Features = features;
            Arguments = new object[] { Features };
        }
    }
}
