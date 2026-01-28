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
    }
}
