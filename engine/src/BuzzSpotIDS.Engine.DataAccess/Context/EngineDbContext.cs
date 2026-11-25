using BuzzSpotIDS.Engine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BuzzSpotIDS.Engine.DataAccess.Context;

public class EngineDbContext : DbContext
{
    public EngineDbContext(
        DbContextOptions<EngineDbContext> options)
        : base(options)
    {
    }

    public DbSet<MonitoredSource> MonitoredSources
        => Set<MonitoredSource>();

    public DbSet<Event> Events
        => Set<Event>();

    public DbSet<HttpEvent> HttpEvents
        => Set<HttpEvent>();

    public DbSet<ApplicationLog> ApplicationLogs
        => Set<ApplicationLog>();

    public DbSet<QueuedEvent> QueuedEvents
        => Set<QueuedEvent>();

    public DbSet<Rule> Rules
        => Set<Rule>();

    public DbSet<RuleVersion> RuleVersions
        => Set<RuleVersion>();

    public DbSet<Alert> Alerts
        => Set<Alert>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("engine");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(EngineDbContext).Assembly);
    }
}