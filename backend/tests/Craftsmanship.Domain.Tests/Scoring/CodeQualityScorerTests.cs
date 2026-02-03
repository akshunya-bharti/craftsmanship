using Craftsmanship.Domain.Scoring;
using Xunit;

namespace Craftsmanship.Domain.Tests.Scoring
{
    public class CodeQualityScorerTests
    {
        [Fact]
        public void Evaluate_Should_Not_Return_100_For_Moderate_Input()
        {
            var scorer = new CodeQualityScorer();

            var rawPayload = """
            {
                "Payload": {
                    "smells": {
                    "critical": 0,
                    "major": 5,
                    "minor": 14
                    },
                    "complexity": {
                    "average": 5.2,
                    "max": 19,
                    "filesAboveThreshold": 3
                    },
                    "duplication": {
                    "percentage": 6,
                    "blocks": 4
                    },
                    "hygiene": {
                    "unusedCode": 3,
                    "suppressedWarnings": 2,
                    "highSeverityIssues": 1
                    },
                    "loc": 24000
                }
            }
            """;

            var result = scorer.Evaluate(rawPayload);

            Assert.InRange(result.OverallScore, 70, 90);
        }
    }
}
