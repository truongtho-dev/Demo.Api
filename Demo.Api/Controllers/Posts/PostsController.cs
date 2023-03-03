using Demo.Api.Attributes;
using Demo.Api.Config;
using Demo.Api.Filters;
using Demo.Api.Proxies.Post;
using Demo.Api.Securities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Controllers.Posts
{
	public class PostsController : BaseController
	{
		private readonly IPost _postClient;
		public PostsController(IPost postClient)
		{
			_postClient = postClient;
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
		[AccountPermission(Feature.Feature3)]
		[HttpGet("{id}")]
		public async Task<Post> GetPostById(int id)
		{
			var post = await _postClient.GetPostById(id);
			return post;
		}
		
	}
}
