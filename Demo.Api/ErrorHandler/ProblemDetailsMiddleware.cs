using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Demo.Api.ErrorHandler
{
    public class ProblemDetailsMiddleware: IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ProblemDetailsMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var problemDetails = new ProblemDetails
                {
                    Status = context.Response.StatusCode,
                    ErrorCode = context.Response.StatusCode.ToString(),
                    ErrorDetails = ex.Message,
                    TraceId = context.Response.StatusCode.ToString(),
                };

                var problemJson = JsonConvert.SerializeObject(problemDetails, _serializerSettings);
                await context.Response.WriteAsync(problemJson);
            }
        }
    }
}
