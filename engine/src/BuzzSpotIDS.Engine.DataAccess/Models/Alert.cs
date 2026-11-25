using BuzzSpotIDS.Engine.DataAccess.Enums;
using BuzzSpotIDS.Engine.DataAccess.Models;

public class Alert
{
    public Guid Id { get; set; }

    public Guid EventId { get; set; }
    public Event Event { get; set; } = null!;

    public Guid RuleVersionId { get; set; }
    public RuleVersion RuleVersion { get; set; } = null!;

    public AlertStatus Status { get; set; } = AlertStatus.New;

    public string? EvidenceJson { get; set; }

    public DateTimeOffset DetectedAt { get; set; }
}