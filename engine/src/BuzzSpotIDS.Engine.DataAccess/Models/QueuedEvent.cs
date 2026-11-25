namespace BuzzSpotIDS.Engine.DataAccess.Models;

public class QueuedEvent
{
    public long Id { get; set; }

    public Guid EventId { get; set; }
    public Event Event { get; set; } = null!;

    public DateTimeOffset EnqueuedAt { get; set; }
    public DateTimeOffset AvailableAt { get; set; }

    public int AttemptCount { get; set; }

    public string? LockedBy { get; set; }
    public Guid? LeaseToken { get; set; }
    public DateTimeOffset? LockedUntil { get; set; }

    public DateTimeOffset? DeadLetteredAt { get; set; }

    public string? LastError { get; set; }
}