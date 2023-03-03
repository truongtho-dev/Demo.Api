using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Demo.Api.Securities
{
	public interface IIdentityContextBuilder
	{
		IdentityContext BuildAsync(HttpContext context);
	}
}
