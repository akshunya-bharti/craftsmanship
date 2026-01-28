using System;
using Craftsmanship.Domain.Ingestion;

namespace Craftsmanship.Infrastructure.Persistence.Entities
{
    public class ScanEntity
    {
        public Guid Id { get; set; }
        public string TeamKey { get; set; } = default!;
        public QualityAspect Aspect { get; set; }
        public string CodebasePath { get; set; } = default!;
        public string CommitId { get; set; } = default!;
        public DateTime ScanTimestamp { get; set; }
        public string RawPayload { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
