using System.Net;

namespace BuzzSpotIDS.Engine.DataAccess.Models;

using System.Net;

public class HttpEvent : Event
{
    public string Method { get; set; } = null!;
    public string Scheme { get; set; } = null!;
    public string Host { get; set; } = null!;
    public string Path { get; set; } = null!;

    public string? QueryString { get; set; }

    public IPAddress? ClientIp { get; set; }

    public int? StatusCode { get; set; }
    public long? DurationMs { get; set; }

    public long? RequestSizeBytes { get; set; }
    public long? ResponseSizeBytes { get; set; }

    public string? RequestHeadersJson { get; set; }
    public string? ResponseHeadersJson { get; set; }

    public string? RequestBodySample { get; set; }
    public string? ResponseBodySample { get; set; }
}