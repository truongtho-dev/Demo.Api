using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CachingService
{
	public interface IDistributedCacheService
	{
		Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class;
		Task SetAsync<T>(string key, T value, TimeSpan? expiredIn, CancellationToken cancellationToken = default) where T : class;
	}
}
