using CachingService;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Api.Caching
{
	public class DistributedCacheService
	{
		private readonly IBaseDistributedCache _cache;
        private readonly ILogger<DistributedCacheService> _logger;

        public DistributedCacheService(IBaseDistributedCache cache, ILogger<DistributedCacheService> logger)
        {
            _logger = logger;
            _cache = cache;
        }

        public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T: class {
			try
			{
				var data = await _cache.GetAsync<T>(key, cancellationToken);
				return data;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Fail to get cache key: {key}. Exception: {ex.Message}");
				return null;
			}
        }

		public async Task SetAsync<T>(string key, T value, TimeSpan? expiredIn, CancellationToken cancellationToken = default) where T: class
		{
			await _cache.SetAsync(key, value, expiredIn, cancellationToken);
		}

    }
}
