using Demo.Api.Controllers.Posts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Api.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddPostOption(this IServiceCollection services) // IConfiguration configuration)
		{
			var postOption = new PostOptions();
			
			services.AddScoped<IPostService, PostService>();
			// services.Configure<PostOptions>(configuration.GetSection("PostOptions"));
			services.Configure<PostOptions>(option => 
			{
				option.Number = postOption.Number;
				option.Type = postOption.Type;
			});

			return services;
		}
	}
}
