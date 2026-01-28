using Craftsmanship.Domain.Ingestion;

namespace Craftsmanship.Infrastructure.Persistence.Entities
{
    public class ScoreSnapshotEntity
    {
        public Guid Id { get; set; }
        public Guid ScanId { get; set; }
        public string TeamKey { get; set; } = default!;
        public QualityAspect Aspect { get; set; }
        public int OverallScore { get; set; }
        public string SubScores { get; set; } = default!;
        public DateTime EvaluatedAt { get; set; } = DateTime.UtcNow;
    }
}
