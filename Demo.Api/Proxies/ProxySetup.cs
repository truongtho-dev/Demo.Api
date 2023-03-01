using Demo.Api.Proxies.Comment;
using Demo.Api.Proxies.Post;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Proxies
{
	public static class ProxySetup
	{
		public static IHttpClientBuilder AddProxy<TProxy>(this IServiceCollection services, IConfiguration configuration, RefitSettings settings = null) where TProxy : class
		{
			var builder = services.AddRefitClient<TProxy>(settings)
				.ConfigureHttpClient(c =>
				{
					c.BaseAddress = new Uri(configuration.GetValue<string>($"EndPoints:FakeApi"));
				});

			return builder;
		}

		public static IServiceCollection AddProxies(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddProxy<IPost>(configuration);
			services.AddProxy<IComment>(configuration);
			return services;
		}
	}
}
