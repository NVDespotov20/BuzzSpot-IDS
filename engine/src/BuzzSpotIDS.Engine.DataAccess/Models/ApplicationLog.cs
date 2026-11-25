using BuzzSpotIDS.Engine.DataAccess.Enums;

namespace BuzzSpotIDS.Engine.DataAccess.Models;

public class ApplicationLog : Event
{
    public LogLevel Level { get; set; }

    public string Message { get; set; } = null!;
    public string? Category { get; set; }
    public string? Exception { get; set; }

    public string? PropertiesJson { get; set; }
}