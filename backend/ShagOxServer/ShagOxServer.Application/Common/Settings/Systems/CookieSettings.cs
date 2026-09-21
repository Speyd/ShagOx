namespace ShagOxServer.Application.Common.Settings.Systems;
public abstract class CookieSettings
{
    public string CookieKey { get; set; } = null!;
    public bool CookieHttpOnly { get; set; }
    public bool CookieSecure { get; set; }
    public int CookieExpireDays { get; set; }
}