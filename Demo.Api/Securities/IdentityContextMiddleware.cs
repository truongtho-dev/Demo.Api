using Demo.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Demo.Api.Securities
{
	public class IdentityContextMiddleware
	{
		private readonly RequestDelegate _next;

		public IdentityContextMiddleware(RequestDelegate next) =>  _next = next;

		public async Task Invoke(HttpContext context, IIdentityContextBuilder identityContextBuilder)
		{
			if(context != null) // & context.User.Identity.IsAuthenticated
			{
				var identityContext = identityContextBuilder.BuildAsync(context);
				context.AddIdentityContext(identityContext);
			}
			await _next(context);
		}
	}

	public static class IdentityContextMiddlewareExtensions
	{
		public static IApplicationBuilder UseIdentityContext(this IApplicationBuilder app)
		{
			app.UseMiddleware<IdentityContextMiddleware>();
			return app;
		}
	}
}
