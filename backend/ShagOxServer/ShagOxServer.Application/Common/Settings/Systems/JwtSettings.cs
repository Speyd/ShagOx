namespace ShagOxServer.Application.Common.Settings.Systems;
public sealed class JwtSettings
    : CookieSettings
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpireMinutes { get; set; }
}