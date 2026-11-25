namespace BuzzSpotIDS.Engine.DataAccess.Models;

public class Rule
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public bool IsEnabled { get; set; } = true;

    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<RuleVersion> Versions { get; set; } = [];
}