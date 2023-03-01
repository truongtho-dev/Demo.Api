using Demo.Api.Proxies.Comment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Controllers.Comments
{
	public class CommentsController : BaseController
	{
		private readonly IComment _commentClient;
		public CommentsController(IComment commentClient)
		{
			_commentClient = commentClient;
		}

		[HttpGet]
		public async Task<List<Comment>> GetAllComments()
		{
			var comments = await _commentClient.GetAllComments();
			return comments;
		}

		[HttpGet("{id}")]
		public async Task<Comment> GetCommentById(int id)
		{
			var comment = await _commentClient.GetCommentById(id);
			return comment;
		}
	}
}
