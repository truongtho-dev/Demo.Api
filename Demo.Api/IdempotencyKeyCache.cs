using CachingService;
using Demo.Api.Constants;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Api
{
	public interface IIdempotencyKeyCache { }
	public class IdempotencyKeyCache : DistributedCacheService
	{
		public async Task AddKeyAsync(Guid id, string idempotencyKey, CancellationToken cancellationToken)
		{
			var key = string.Format(AppConstant.IDEMPOTENCY_KEY, id, idempotencyKey);
			await SetAsync(key, idempotencyKey, TimeSpan.FromMinutes(10), cancellationToken);
		}
	}

	public async Task IsExists(Guid id, string idempotencyKey, CancellationToken cancellationToken)
	{
		var key = string.Format(AppConstant.IDEMPOTENCY_KEY, id, idempotencyKey);
		var storedKey = await this.GetAsync(key, cancellationToken);
	}
}
