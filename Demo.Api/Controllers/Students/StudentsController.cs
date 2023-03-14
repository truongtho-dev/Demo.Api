using Demo.Api.Attributes;
using Demo.Api.Config;
using Demo.Api.Constants;
using Demo.Api.Model;
using Demo.Api.Securities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Api.Controllers.Students
{
	public class StudentsController : BaseController // for test IdempotencyKey
	{
		private readonly ILogger<StudentsController> _logger;
        private readonly AppDbContext _context; // without repository, handle error, validation for simple

        public StudentsController(ILogger<StudentsController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

		[AppRole(AppRoles.App)]
		[AccountPermission(Feature.Feature1, Feature.Feature2)]
		[HttpGet]
        public async Task<List<Student>> GetAllStudents()
        {
            var result = await _context.Students.ToListAsync();
            return result;
        }

		[AppRole(AppRoles.App)]
		[AccountPermission(Feature.Feature1, Feature.Feature2)]
        [Idempotency(IdempotencyType.Student)]
        [HttpPost]
        public async Task<IActionResult> CreateStudent(
            [FromBody] Student model,
            [FromHeader(Name = AppConstant.IDEMPOTENCY_NAME)] Guid? idempotencyKey
            )
        {
			model.IdempotencyKey = idempotencyKey ?? Guid.NewGuid();
			var result = _context.Students.Add(model);
			_ = await _context.SaveChangesAsync();
            return Ok(result);
        }
	}
}
