namespace ShagOxServer.Application.Common.Options.Systems;
public sealed class JwtOptions
    : CookieOptions
{
    public new const string SectionName = "Jwt";

    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpireMinutes { get; set; }
}
