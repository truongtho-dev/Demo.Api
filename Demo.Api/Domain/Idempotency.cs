using System;

namespace Demo.Api.Domain
{
	public class Idempotency
	{
		public Guid Id { get; set; }
		public Guid AccountId { get; set; }
		public string Key { get; set; }
	}
}
