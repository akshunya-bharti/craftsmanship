using Craftsmanship.Api.Models;
using Craftsmanship.Infrastructure.Persistence;
using Craftsmanship.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Craftsmanship.Api.Controllers
{
    [ApiController]
    [Route("api/teams")]
    public class TeamsController : ControllerBase
    {
        private readonly CraftsmanshipDbContext _dbContext;

        public TeamsController(CraftsmanshipDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamResponse>>> GetTeams()
        {
            var teams = await _dbContext.Teams
                .OrderBy(t => t.Name)
                .Select(t => new TeamResponse
                {
                    TeamKey = t.TeamKey,
                    Name = t.Name
                })
                .ToListAsync();

            return Ok(teams);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamRequest request)
        {
            // Basic validation (v1)
            if (string.IsNullOrWhiteSpace(request.TeamKey) ||
                string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("TeamKey and Name are required.");
            }

            var exists = await _dbContext.Teams
                .AnyAsync(t => t.TeamKey == request.TeamKey);

            if (exists)
            {
                return Conflict($"Team with key '{request.TeamKey}' already exists.");
            }

            var team = new TeamEntity
            {
                Id = Guid.NewGuid(),
                TeamKey = request.TeamKey,
                Name = request.Name
            };

            _dbContext.Teams.Add(team);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTeams),
                new { teamKey = team.TeamKey },
                null
            );
        }
    }
}