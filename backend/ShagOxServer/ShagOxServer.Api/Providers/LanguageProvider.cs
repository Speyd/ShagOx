using ShagOxServer.Application.Interfaces.Providers;

namespace ShagOxServer.Api.Providers;
public class LanguageProvider(IHttpContextAccessor httpContextAccessor)
    : ILanguageProvider
{
    public string Language =>
        httpContextAccessor.HttpContext?
            .Request.Headers["Accept-Language"]
            .ToString() ?? "uk";
}