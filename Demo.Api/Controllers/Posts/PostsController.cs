using Demo.Api.Attributes;
using Demo.Api.Config;
using Demo.Api.Proxies.Post;
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
		[HttpGet]
		public async Task<List<Post>> GetAllPosts()
		{
			var posts = await _postClient.GetAllPosts();
			return posts;
		}

		[AppRole(AppRoles.App)]
		[HttpGet("{id}")]
		public async Task<Post> GetPostById(int id)
		{
			var post = await _postClient.GetPostById(id);
			return post;
		}
		
	}
}
