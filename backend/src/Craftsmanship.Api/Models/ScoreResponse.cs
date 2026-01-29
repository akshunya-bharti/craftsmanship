using System;

namespace Craftsmanship.Api.Models
{
    public class ScoreResponse
    {
        public string TeamKey { get; set; } = default!;
        public string Aspect { get; set; } = default!;
        public int OverallScore { get; set; }
        public DateTime EvaluatedAt { get; set; }
    }
}