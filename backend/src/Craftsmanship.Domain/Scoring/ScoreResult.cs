namespace Craftsmanship.Domain.Scoring
{
    public class ScoreResult
    {
        public int OverallScore { get; set; }
        public object SubScores { get; set; } = default!;
    }
}
