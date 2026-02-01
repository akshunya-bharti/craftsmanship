using Craftsmanship.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Craftsmanship.Infrastructure.Persistence.Configurations
{
    public class ScanEntityConfiguration : IEntityTypeConfiguration<ScanEntity>
    {
        public void Configure(EntityTypeBuilder<ScanEntity> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.TeamKey)
                   .IsRequired();

            builder.Property(s => s.Aspect)
                   .IsRequired();

            builder.Property(s => s.RawPayload)
                   .IsRequired();

            // 🔐 Foreign Key: Scans.TeamKey → Teams.TeamKey
            builder.HasOne<TeamEntity>()
                   .WithMany()
                   .HasForeignKey(s => s.TeamKey)
                   .HasPrincipalKey(t => t.TeamKey)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}