using System.Text.Json;
using Craftsmanship.Domain.Ingestion;
using Craftsmanship.Domain.Scoring;
using Craftsmanship.Infrastructure.Persistence;
using Craftsmanship.Infrastructure.Persistence.Entities;
using Craftsmanship.Infrastructure.Scoring;
using Microsoft.EntityFrameworkCore;

namespace Craftsmanship.Worker;

public class Worker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public Worker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<CraftsmanshipDbContext>();

            var unprocessedScans = await db.Scans
                .Where(s => !s.IsProcessed)
                .Take(5)
                .ToListAsync(stoppingToken);

            foreach (var scan in unprocessedScans)
            {
                var scorer = GetScorer(scan.Aspect);

                if (scorer == null)
                    continue;

                var result = scorer.Evaluate(scan.RawPayload);

                db.ScoreSnapshots.Add(new ScoreSnapshotEntity
                {
                    Id = Guid.NewGuid(),
                    ScanId = scan.Id,
                    TeamKey = scan.TeamKey,
                    Aspect = scan.Aspect,
                    OverallScore = result.OverallScore,
                    SubScores = JsonSerializer.Serialize(result.SubScores)
                });

                scan.IsProcessed = true;
            }

            await db.SaveChangesAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }

    private static IScorer? GetScorer(QualityAspect aspect)
    {
        return aspect switch
        {
            QualityAspect.CodeQuality => new CodeQualityScorer(),
            _ => null
        };
    }
}