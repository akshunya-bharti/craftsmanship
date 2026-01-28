using Craftsmanship.Domain.Ingestion;
using Craftsmanship.Domain.Scoring;

namespace Craftsmanship.Infrastructure.Scoring
{
    public class CodeQualityScorer : IScorer
    {
        public QualityAspect Aspect => QualityAspect.CodeQuality;

        public ScoreResult Evaluate(string rawPayload)
        {
            // v1: stub logic
            return new ScoreResult
            {
                OverallScore = 75,
                SubScores = new
                {
                    Smells = 70,
                    Complexity = 80,
                    Duplication = 75,
                    Hygiene = 70,
                    Trend = 80
                }
            };
        }
    }
}
