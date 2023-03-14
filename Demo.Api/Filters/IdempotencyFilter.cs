using Demo.Api.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Demo.Api.ErrorHandler;
using System.Linq;
using Demo.Api.Extensions;
using Demo.Api.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using Demo.Api.Constants;

namespace Demo.Api.Filters
{
	public class IdempotencyFilter : IAsyncActionFilter
	{
		private readonly IdempotencyType[] _idempotencyTypes;
		private readonly ILogger<IdempotencyFilter> _logger;
		private readonly IIdempotencyKeyCache _idempotencyKeyCache;
		private readonly AppDbContext _context;

		public IdempotencyFilter(
			IdempotencyType[] idempotencyTypes, 
			ILogger<IdempotencyFilter> logger,
			IIdempotencyKeyCache idempotencyKeyCache,
			AppDbContext context)
        {
            _idempotencyTypes = idempotencyTypes;
			_logger = logger;
			_idempotencyKeyCache = idempotencyKeyCache;
			_context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if(!context.HttpContext.Request.Headers.TryGetValue(AppConstant.IDEMPOTENCY_NAME, out var idempotencyKeys))
			{
				_logger.LogError("Idempotency key is required");
				context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
				context.Result = BadRequestDetail("Idempotency key is invalid");
			}

			var identityContext = context.HttpContext.GetIdentityContext();

			var idempotencyKey = idempotencyKeys.First();

			if (_idempotencyTypes.Any())
			{
				if (identityContext != null && await _idempotencyKeyCache.IsExists(identityContext.IdentityId, idempotencyKey))
				{
					_logger.LogError("Idempotency key is existed in cache");
					context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
					context.Result = BadRequestDetail("Duplicate requesting");
					return;
				}

				var existedIdempotencyKey = await _context.Set<Idempotency>().AnyAsync(ide => ide.Key == idempotencyKey);
				if (existedIdempotencyKey)
				{
					_logger.LogInformation($"Idempotency - {existedIdempotencyKey} is existed.");
					return;
				}

				var model = new Idempotency
				{
					Key = idempotencyKey,
					AccountId = identityContext.AccountId
				};

				await _idempotencyKeyCache.AddKeyAsync(identityContext.AccountId, idempotencyKey);
				CreateIdempotencyAsync(context, model);
			}

			await next();
		}

		private void CreateIdempotencyAsync(ActionExecutingContext context, Idempotency model)
		{
			try
			{
				_context.Set<Idempotency>().Add(model);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				var errMsg = $"Error when create IdempotencyKey: {model.Key}";
				_logger.LogError(ex, errMsg);
				context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
				context.Result = BadRequestDetail(errMsg);
			}
		}

		private BadRequestObjectResult BadRequestDetail(string message, string detail = default)
		{
			var problemDetails = new ProblemDetail(detail ?? message)
			{
				ErrorCode = nameof(message)
			};
			return new BadRequestObjectResult(problemDetails);
		}
	}
}
