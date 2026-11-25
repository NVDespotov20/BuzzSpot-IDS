namespace BuzzSpotIDS.Engine.DataAccess.Models;

public abstract class Event
{
    public Guid Id { get; set; }

    public Guid SourceId { get; set; }
    public MonitoredSource Source { get; set; } = null!;

    public DateTimeOffset OccurredAt { get; set; }
    public DateTimeOffset ReceivedAt { get; set; }

    public string? TraceId { get; set; }
    public string? SpanId { get; set; }

    public QueuedEvent? QueuedEvent { get; set; }

    public ICollection<Alert> Alerts { get; set; } = [];
}