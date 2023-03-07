using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingService
{
	public static class CacheSetup
	{
		public static IServiceCollection AddDistributedCache(this IServiceCollection services, DistributedCacheOption options)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));

			switch (options.Type)
			{
				case CacheType.Memory:
					services.AddDistributedMemoryCache();
					break;
				case CacheType.Redis:
					services.AddStackExchangeRedisCache(opt =>
					{
						opt.InstanceName = options.AppName;
						opt.Configuration = options.ConnectionString;
					});
					break;
			}
			return services.AddSingleton<IDistributedCacheService, DistributedCacheService>();
		}

		public static IServiceCollection AddDistributedCache(this IServiceCollection services, IConfiguration configuration)
		{
			var options = configuration.GetSection(DistributedCacheOption.Name).Get<DistributedCacheOption>();
			return services.AddDistributedCache(options);
		}
	}
}
