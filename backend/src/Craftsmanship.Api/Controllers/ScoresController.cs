using Craftsmanship.Api.Models;
using Craftsmanship.Domain.Ingestion;
using Craftsmanship.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Craftsmanship.Api.Controllers
{
    [ApiController]
    [Route("api/teams")]
    public class ScoresController : ControllerBase
    {
        private readonly CraftsmanshipDbContext _dbContext;

        public ScoresController(CraftsmanshipDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{teamKey}/scores/current")]
        public async Task<ActionResult<ScoreResponse>> GetCurrentScore(string teamKey)
        {
            var latestScore = await _dbContext.ScoreSnapshots
                .Where(s => s.TeamKey == teamKey && s.Aspect == QualityAspect.CodeQuality)
                .OrderByDescending(s => s.EvaluatedAt)
                .FirstOrDefaultAsync();

            if (latestScore == null)
                return NotFound();

            return new ScoreResponse
            {
                TeamKey = latestScore.TeamKey,
                Aspect = latestScore.Aspect.ToString(),
                OverallScore = latestScore.OverallScore,
                EvaluatedAt = latestScore.EvaluatedAt
            };
        }
    }
}
