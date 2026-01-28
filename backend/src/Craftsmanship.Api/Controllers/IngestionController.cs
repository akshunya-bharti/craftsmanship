using System.Text.Json;
using Craftsmanship.Domain.Ingestion;
using Craftsmanship.Infrastructure.Persistence;
using Craftsmanship.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Craftsmanship.Api.Controllers
{
    [ApiController]
    [Route("api/ingestion")]
    public class IngestionController : ControllerBase
    {
        private readonly CraftsmanshipDbContext _dbContext;

        public IngestionController(CraftsmanshipDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("scans")]
        public async Task<IActionResult> IngestScan([FromBody] QualitySnapshot snapshot)
        {
            if (string.IsNullOrWhiteSpace(snapshot.TeamKey) ||
                string.IsNullOrWhiteSpace(snapshot.CommitId))
            {
                return BadRequest("TeamKey and CommitId are required.");
            }

            var scan = new ScanEntity
            {
                Id = Guid.NewGuid(),
                TeamKey = snapshot.TeamKey,
                Aspect = snapshot.Aspect,
                CodebasePath = snapshot.CodebasePath,
                CommitId = snapshot.CommitId,
                ScanTimestamp = snapshot.ScanTimestamp,
                RawPayload = JsonSerializer.Serialize(snapshot)
            };

            _dbContext.Scans.Add(scan);
            await _dbContext.SaveChangesAsync();

            return Accepted();
        }
    }
}