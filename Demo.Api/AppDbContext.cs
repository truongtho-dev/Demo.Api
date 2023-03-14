using Demo.Api.Domain;
using Demo.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Demo.Api
{
	public class AppDbContext: DbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Idempotency> Idempotencies { get; set; }
	}
}
