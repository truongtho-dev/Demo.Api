using Demo.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.Api.Attributes
{
	public class IdempotencyAttribute: TypeFilterAttribute
	{
        public IdempotencyAttribute(params IdempotencyType[] idempotencyTypes): base(typeof(IdempotencyFilter))
        {
            Arguments = new object[] { idempotencyTypes };
        }
    }

    public enum IdempotencyType
    {
        Student = 1, Post = 2, Comment = 3
    }
}
