using Craftsmanship.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Craftsmanship.Infrastructure.Persistence.Configurations
{
    public class TeamEntityConfiguration : IEntityTypeConfiguration<TeamEntity>
    {
        public void Configure(EntityTypeBuilder<TeamEntity> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasIndex(t => t.TeamKey)
                   .IsUnique();

            builder.Property(t => t.TeamKey)
                   .IsRequired();

            builder.Property(t => t.Name)
                   .IsRequired();

            builder.Property(t => t.CreatedAt)
                   .IsRequired();
        }
    }
}
