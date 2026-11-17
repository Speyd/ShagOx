namespace ShagOxServer.Application.Common.Options.Systems;
public abstract class CookieOptions
{
    public string CookieKey { get; set; } = null!;
    public bool CookieHttpOnly { get; set; }
    public bool CookieSecure { get; set; }
    public int CookieExpireDays { get; set; }
}
