using Demo.Api.Attributes;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Demo.Api.Filters
{
	public class IdempotencyFilter : IAsyncActionFilter
	{
		private const string IDEMPOTENCY_HEADER_KEY = "idempotency-key";
		private readonly IdempotencyType[] _idempotencyTypes;

        public IdempotencyFilter(IdempotencyType[] idempotencyTypes)
        {
            _idempotencyTypes = idempotencyTypes;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			// lay idempotency-key o header

		}
	}
}
