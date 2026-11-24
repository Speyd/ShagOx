namespace ShagOxServer.Application.Common.Options.Systems.Cookies;
public sealed class RefreshTokenOptions
{
    public const string SectionName = "RefreshToken";

    public int ExpireDays { get; set; }

    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
}
