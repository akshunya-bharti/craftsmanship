using Craftsmanship.Api.Models;
using Craftsmanship.Infrastructure.Persistence;
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
    }
}