using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Demo.Api.Filters
{
	public class CustomFilter: IActionFilter
	{
		private readonly ILogger<CustomFilter> _logger;

		public CustomFilter(ILogger<CustomFilter> logger)
		{
			_logger = logger;
		}
		public void OnActionExecuted(ActionExecutedContext context)
		{
			_logger.LogInformation("Do something after the action method is called");
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			_logger.LogInformation("Do something before the action method is called");
		}
	}
}
