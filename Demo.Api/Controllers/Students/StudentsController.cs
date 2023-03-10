using Demo.Api.Attributes;
using Demo.Api.Config;
using Demo.Api.Model;
using Demo.Api.Securities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Api.Controllers.Students
{
	public class StudentsController : BaseController
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
        [Idempotency()]
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] Student model)
        {
            var result = _context.Students.Add(model);
			_ = await _context.SaveChangesAsync();
            return Ok();
        }
	}
}
