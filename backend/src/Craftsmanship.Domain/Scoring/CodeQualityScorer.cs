using System.Text.Json;
using Craftsmanship.Domain.CodeQuality;
using Craftsmanship.Domain.Ingestion;

namespace Craftsmanship.Domain.Scoring
{
    public class CodeQualityScorer : IScorer
    {

        public QualityAspect Aspect => QualityAspect.CodeQuality;

        private static readonly JsonSerializerOptions JsonOptions =
        new()
        {
            PropertyNameCaseInsensitive = true
        };


        public ScoreResult Evaluate(string rawPayload)
        {
            var snapshot = JsonSerializer.Deserialize<QualitySnapshotWrapper>(
                rawPayload,
                JsonOptions
            ) ?? throw new InvalidOperationException("Failed to deserialize wrapper");

            var data = JsonSerializer.Deserialize<CodeQualitySnapshot>(
                snapshot.Payload.GetRawText(),
                JsonOptions
            ) ?? throw new InvalidOperationException("Failed to deserialize code quality snapshot");

            var smellScore = CalculateSmellScore(data);
            var complexityScore = CalculateComplexityScore(data);
            var duplicationScore = CalculateDuplicationScore(data);
            var hygieneScore = CalculateHygieneScore(data);

            var overall =
                smellScore * 0.30 +
                complexityScore * 0.25 +
                duplicationScore * 0.20 +
                hygieneScore * 0.25;

            return new ScoreResult
            {
                OverallScore = (int)Math.Round(overall),
                SubScores = new
                {
                    Smells = smellScore,
                    Complexity = complexityScore,
                    Duplication = duplicationScore,
                    Hygiene = hygieneScore
                }
            };
        }

        private int CalculateSmellScore(CodeQualitySnapshot d)
        {
            var penalty =
                d.Smells.Critical * 10 +
                d.Smells.Major * 2 +
                d.Smells.Minor * 0.5;

            return Clamp(100 - penalty / Math.Max(d.Loc / 1000, 1));
        }

        private int CalculateComplexityScore(CodeQualitySnapshot d)
        {
            var penalty =
                d.Complexity.Max > 25 ? 20 : 0 +
                d.Complexity.FilesAboveThreshold * 3;

            return Clamp(100 - penalty);
        }

        private int CalculateDuplicationScore(CodeQualitySnapshot d)
        {
            var penalty =
                d.Duplication.Percentage * 2 +
                d.Duplication.Blocks;

            return Clamp(100 - penalty);
        }

        private int CalculateHygieneScore(CodeQualitySnapshot d)
        {
            var penalty =
                d.Hygiene.HighSeverityIssues * 10 +
                d.Hygiene.UnusedCode * 2 +
                d.Hygiene.SuppressedWarnings;

            return Clamp(100 - penalty);
        }

        private int Clamp(double value)
            => (int)Math.Max(0, Math.Min(100, value));
    }

    internal class QualitySnapshotWrapper
    {
        public JsonElement Payload { get; set; } = default!;
    }
}
