using System;

namespace Craftsmanship.Infrastructure.Persistence.Entities
{
    public class TeamEntity
    {
        public Guid Id { get; set; }

        public string TeamKey { get; set; } = default!;
        public string Name { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}