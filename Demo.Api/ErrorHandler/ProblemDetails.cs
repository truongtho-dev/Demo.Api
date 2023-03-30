using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Demo.Api.ErrorHandler
{
	public class ProblemDetails
	{
		private const string INVALID_ARGUMENT = "invalid_argument";

        public ProblemDetails(string errorMessage)
		{
			ErrorCode = INVALID_ARGUMENT;
			ErrorMessage = errorMessage;
		}

		public ProblemDetails()
		{
		}

		public int? Status { get; set; }

		public string TraceId { get; set; }

		public string ErrorCode { get; set; }

		public string ErrorMessage { get; set; }

		public IDictionary<string, string[]> ErrorDetails { get; set; } = new Dictionary<string, string[]>();

		public bool ShouldSerializeErrorDetails()
		{
			var errorDetails = ErrorDetails;
			return errorDetails.Count > 0;
		}
	}
}
