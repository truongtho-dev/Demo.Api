using System.Collections.Generic;

namespace Demo.Api.ErrorHandler
{
	public class ProblemDetail
	{
		private const string INVALID_ARGUMENT = "invalid_argument";

		public ProblemDetail(string errorMessage)
		{
			ErrorCode = "invalid_argument";
			ErrorMessage = errorMessage;
		}

		public ProblemDetail()
		{
		}

		public int? Status { get; set; }

		public string TraceId { get; set; }

		public string ErrorCode { get; set; }

		public string ErrorMessage { get; set; }

		public IDictionary<string, string[]> ErrorDetails { get; set; } = (IDictionary<string, string[]>)new Dictionary<string, string[]>();

		public bool ShouldSerializeErrorDetails()
		{
			var errorDetails = ErrorDetails;
			return errorDetails.Count > 0;
		}
	}
}
