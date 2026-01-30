namespace Craftsmanship.Domain.CodeQuality
{
    public class CodeQualitySnapshot
    {
        public CodeSmells Smells { get; set; } = new();
        public ComplexityMetrics Complexity { get; set; } = new();
        public DuplicationMetrics Duplication { get; set; } = new();
        public HygieneMetrics Hygiene { get; set; } = new();
        public int Loc { get; set; }
    }

    public class CodeSmells
    {
        public int Critical { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
    }

    public class ComplexityMetrics
    {
        public double Average { get; set; }
        public int Max { get; set; }
        public int FilesAboveThreshold { get; set; }
    }

    public class DuplicationMetrics
    {
        public double Percentage { get; set; }
        public int Blocks { get; set; }
    }

    public class HygieneMetrics
    {
        public int UnusedCode { get; set; }
        public int SuppressedWarnings { get; set; }
        public int HighSeverityIssues { get; set; }
    }
}