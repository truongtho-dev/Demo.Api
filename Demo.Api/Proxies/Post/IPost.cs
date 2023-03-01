using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;

namespace Demo.Api.Proxies.Post
{
	public interface IPost
	{
		[Get("/posts")]
		public Task<List<Post>> GetAllPosts();

		[Get("/posts/{id}")]
		Task<Post> GetPostById(int id);
	}
}
