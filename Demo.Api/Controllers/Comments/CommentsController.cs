using Demo.Api.Attributes;
using Demo.Api.Config;
using Demo.Api.Proxies.Comment;
using Demo.Api.Securities;
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

		[AppRole(AppRoles.App)]
		[AccountPermission(Feature.Feature1)]
		[HttpGet]
		public async Task<List<Comment>> GetAllComments()
		{
			var comments = await _commentClient.GetAllComments();
			return comments;
		}

		[AppRole(AppRoles.App)]
		[AccountPermission(Feature.Feature2)]
		[HttpGet("{id}")]
		public async Task<Comment> GetCommentById(int id)
		{
			var comment = await _commentClient.GetCommentById(id);
			return comment;
		}
	}
}
