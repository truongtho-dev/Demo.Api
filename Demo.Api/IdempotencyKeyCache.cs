using CachingService;
using Demo.Api.Caching;
using Demo.Api.Constants;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Api
{
	public interface IIdempotencyKeyCache {
		Task AddKeyAsync(Guid id, string idempotencyKey);
		Task<bool> IsExists(Guid id, string idempotencyKey);
	}
	public class IdempotencyKeyCache : DistributedCacheService, IIdempotencyKeyCache
	{
		public IdempotencyKeyCache(IBaseDistributedCache cache, ILogger<DistributedCacheService> logger) : base(cache, logger)
		{
		}

		public async Task AddKeyAsync(Guid id, string idempotencyKey)
		{
			var key = string.Format(AppConstant.IDEMPOTENCY_KEY, id, idempotencyKey);
			await SetAsync(key, idempotencyKey, TimeSpan.FromMinutes(10));
		}

		public async Task<bool> IsExists(Guid id, string idempotencyKey)
		{
			var key = string.Format(AppConstant.IDEMPOTENCY_KEY, id, idempotencyKey);
			var storedKey = await GetAsync<string>(key);

			return storedKey != null;
		}
	}

	
}
