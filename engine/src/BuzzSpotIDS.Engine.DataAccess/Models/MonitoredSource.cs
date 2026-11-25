namespace BuzzSpotIDS.Engine.DataAccess.Models;

public class MonitoredSource
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? BaseUrl { get; set; }

    public bool IsEnabled { get; set; } = true;

    public DateTimeOffset CreatedAt { get; set; }
    
    public ICollection<Event> Events { get; set; } = [];
}