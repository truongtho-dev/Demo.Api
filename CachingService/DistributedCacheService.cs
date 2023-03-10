using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CachingService
{
	public class DistributedCacheService: IDistributedCacheService
	{
		private readonly IDistributedCache _cache;
		private readonly ILogger<DistributedCacheService> _logger;

        public DistributedCacheService()
        {
            
        }
        public DistributedCacheService(IDistributedCache cache, ILogger<DistributedCacheService> logger)
		{
			_cache = cache;
			_logger = logger;
		}

		public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
		{
			try
			{
				var data = await _cache.GetAsync(key, cancellationToken);
				if (data == null) return null;
				string stringData = Encoding.Unicode.GetString(data);
				_logger.LogInformation($"Get cache successfully with key: {key}");
				return typeof(T) == typeof(string) ? stringData as T : JsonSerializer.Deserialize<T>(stringData);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed to get cache key: {key}. Exception: {ex.Message}");
				return null;
			}
		}

		public async Task SetAsync<T>(string key, T value, TimeSpan? expiredIn, CancellationToken cancellationToken) where T : class
		{
			var stringData = typeof(T) == typeof(string) ? value.ToString() : JsonSerializer.Serialize(value);
			try
			{
				if (expiredIn == null)
					await _cache.SetAsync(key, Encoding.Unicode.GetBytes(stringData), cancellationToken);
				else
					await _cache.SetAsync(key, Encoding.Unicode.GetBytes(stringData), new DistributedCacheEntryOptions
					{
						AbsoluteExpirationRelativeToNow = expiredIn
					}, cancellationToken);
				_logger.LogInformation($"Set cache successfully with key: {key}");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed to set cache key: {key}. Exception: {ex.Message}");
			}
		}
	}
}
