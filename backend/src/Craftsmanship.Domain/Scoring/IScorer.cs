using Craftsmanship.Domain.Ingestion;

namespace Craftsmanship.Domain.Scoring
{
    public interface IScorer
    {
        QualityAspect Aspect { get; }
        ScoreResult Evaluate(string rawPayload);
    }
}
