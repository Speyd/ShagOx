namespace ShagOxServer.Domain.Entities.Security;
public sealed class IpRateLimitState
{
    public string Ip { get; }

    public int RemainingTokens { get; set; }

    public DateTime LastRequestAt { get; set; }

    public DateTime? BannedUntil { get; set; }


    public IpRateLimitState(
        string ip)
    {
        Ip = ip;
    }
}
