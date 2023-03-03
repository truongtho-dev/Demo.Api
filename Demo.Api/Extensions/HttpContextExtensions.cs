using Demo.Api.Securities;
using Microsoft.AspNetCore.Http;

namespace Demo.Api.Extensions
{
	public static class HttpContextExtensions
	{
		private const string IdentityContextKey = "IdentityContextKey";

		public static void AddIdentityContext(this HttpContext @this, IdentityContext identityContext)
		{
			@this.Items.Add(IdentityContextKey, identityContext);
		}
		public static IdentityContext GetIdentityContext(this HttpContext @this)
		{
			if (@this == null) return null;
			@this.Items.TryGetValue(IdentityContextKey, out var identityContext);

			return (IdentityContext)identityContext;
		}
	}
}
