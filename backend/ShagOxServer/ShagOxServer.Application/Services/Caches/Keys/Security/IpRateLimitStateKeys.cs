namespace ShagOxServer.Application.Services.Caches.Keys.Security;
public static class IpRateLimitStateKeys
{
    public static readonly string Prefix = "user";

    public static string ByIp(
       string userIp)
           => $"{Prefix}:{userIp}";
}
