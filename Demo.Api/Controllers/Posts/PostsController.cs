using CachingService;
using Demo.Api.Attributes;
using Demo.Api.Config;
using Demo.Api.Filters;
using Demo.Api.Proxies.Post;
using Demo.Api.Securities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Api.Controllers.Posts
{
	public class PostsController : BaseController
	{
		private readonly ILogger<PostsController> _logger;	
		private readonly IPost _postClient;
		private readonly IBaseDistributedCache _cacheService;
		
		public PostsController(IPost postClient, IBaseDistributedCache cacheService, ILogger<PostsController> logger)
		{
			_postClient = postClient;
			_cacheService = cacheService;
			_logger = logger;
		}

		[AppRole(AppRoles.App)]
		[AccountPermission(Feature.Feature1, Feature.Feature2)]
		[HttpGet]
		public async Task<List<Post>> GetAllPosts()
		{
			var posts = await _postClient.GetAllPosts();
			return posts;
		}

		[AppRole(AppRoles.App)]
		[AccountPermission(Feature.Feature1, Feature.Feature2)]
		[HttpGet("{id}")]
		public async Task<Post> GetPostById(int id, CancellationToken cancellationToken)
		{
			Post post;
			var cacheKey = $"post_{id}";
			var cacheValue = await _cacheService.GetAsync<Post>(cacheKey, cancellationToken);

			if (cacheValue != null) post = cacheValue;
			else
			{
				post = await _postClient.GetPostById(id);
				if (id == 1)
				{
					var cacheOptions = new DistributedCacheEntryOptions
					{
						AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
					};
					var expiredIn = TimeSpan.FromMinutes(10);
					await _cacheService.SetAsync<Post>(cacheKey, post, expiredIn, cancellationToken);
				}
			}
			return post;
		}

		[AppRole(AppRoles.App)]
		[AccountPermission(Feature.Feature1, Feature.Feature2)]
		[HttpGet("option")]
		public PostOptions GetPostOptions(
			[FromServices] IPostService postService
			)
		{
			return postService.GetPostOptions();
		}
	}
}
