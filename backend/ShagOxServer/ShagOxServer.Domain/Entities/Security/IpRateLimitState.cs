namespace ShagOxServer.Domain.Entities.Security;
public sealed class IpRateLimitState
{
    public string Ip { get; }

    public int RemainingTokens { get; set; } = 0;

    public int HttpRequest { get; set; } = 0;


    public DateTime LastRequestAt { get; set; }

    public DateTime? BannedUntil { get; set; }


    public IpRateLimitState(
        string ip)
    {
        Ip = ip;
        LastRequestAt = DateTime.UtcNow;
    }
}
