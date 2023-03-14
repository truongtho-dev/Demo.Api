using System;

namespace Demo.Api.Model
{
	public class Student
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Guid IdempotencyKey { get; set; }
	}
}
