using Demo.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Api.TypeConfiguration
{
	public class IdempotencyTypeConfiguration : IEntityTypeConfiguration<Idempotency>
	{
		public void Configure(EntityTypeBuilder<Idempotency> builder)
		{
			builder.HasKey(i => i.Id);
		}
	}
}
