using BuzzSpotIDS.Engine.DataAccess.Enums;

namespace BuzzSpotIDS.Engine.DataAccess.Models;

public class RuleVersion
{
    public Guid Id { get; set; }

    public Guid RuleId { get; set; }
    public Rule Rule { get; set; } = null!;

    public int Version { get; set; }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public AlertSeverity Severity { get; set; }

    public string DefinitionJson { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<Alert> Alerts { get; set; } = [];
}