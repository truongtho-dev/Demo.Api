using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;

namespace Demo.Api.Proxies.Comment
{
	public interface IComment
	{
		[Get("/comments")]
		public Task<List<Comment>> GetAllComments();

		[Get("/comments/{id}")]
		public Task<Comment> GetCommentById(int id);
	}
}
