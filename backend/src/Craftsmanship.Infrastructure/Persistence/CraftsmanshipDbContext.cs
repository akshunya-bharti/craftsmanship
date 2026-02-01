using Craftsmanship.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Craftsmanship.Infrastructure.Persistence
{
    public class CraftsmanshipDbContext : DbContext
    {
        public CraftsmanshipDbContext(DbContextOptions<CraftsmanshipDbContext> options)
            : base(options)
        {
        }

        public DbSet<ScanEntity> Scans => Set<ScanEntity>();
        public DbSet<ScoreSnapshotEntity> ScoreSnapshots => Set<ScoreSnapshotEntity>();
        public DbSet<TeamEntity> Teams => Set<TeamEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(CraftsmanshipDbContext).Assembly
            );
        }

        public override int SaveChanges()
        {
            Database.ExecuteSqlRaw("PRAGMA foreign_keys = ON;");
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await Database.ExecuteSqlRawAsync("PRAGMA foreign_keys = ON;", cancellationToken);
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
