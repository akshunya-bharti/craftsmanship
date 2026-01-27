using Microsoft.EntityFrameworkCore;

namespace Craftsmanship.Infrastructure.Persistence
{
    public class CraftsmanshipDbContext : DbContext
    {
        public CraftsmanshipDbContext(DbContextOptions<CraftsmanshipDbContext> options)
            : base(options)
        {
        }

        // Placeholder – entities will come later
        // public DbSet<Team> Teams { get; set; }
    }
}
