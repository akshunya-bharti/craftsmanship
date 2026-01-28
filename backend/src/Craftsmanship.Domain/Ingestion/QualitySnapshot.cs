using System;

namespace Craftsmanship.Domain.Ingestion
{
    public class QualitySnapshot
    {
        public string SchemaVersion { get; set; } = "1.0";
        public QualityAspect Aspect { get; set; }
        public string TeamKey { get; set; } = default!;
        public string Repository { get; set; } = default!;
        public string CodebasePath { get; set; } = default!;
        public string Branch { get; set; } = "main";
        public string CommitId { get; set; } = default!;
        public DateTime ScanTimestamp { get; set; }
        public object Payload { get; set; } = default!;
    }
}