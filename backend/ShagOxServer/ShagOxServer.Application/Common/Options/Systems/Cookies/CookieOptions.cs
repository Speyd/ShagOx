using Microsoft.Net.Http.Headers;

namespace ShagOxServer.Application.Common.Options.Systems.Cookies;
public abstract class CookieOptions
{
    public const string SectionName = "Cookie";

    public bool HttpOnly { get; set; } = true;
    public bool Secure { get; set; } = true;

    public SameSiteMode SameSite { get; set; }

    public string AccessTokenKey { get; set; } = "access_token";
    public string RefreshTokenKey { get; set; } = "refresh_token";
}
