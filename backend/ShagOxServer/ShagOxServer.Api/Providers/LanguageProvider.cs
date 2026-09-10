using ShagOxServer.Application.Interfaces.Providers;
using System.Globalization;

namespace ShagOxServer.Api.Providers;
public class LanguageProvider(IHttpContextAccessor httpContextAccessor)
    : ILanguageProvider
{
    private readonly string BaseLanguage = "en";

    public string Language
    {
        get
        {
            var acceptLanguage = httpContextAccessor.HttpContext?
                .Request.Headers["Accept-Language"]
                .ToString();

            if (string.IsNullOrWhiteSpace(acceptLanguage))
                return BaseLanguage;

            var language = acceptLanguage
                .Split(',')
                .FirstOrDefault()?
                .Split(';')
                .FirstOrDefault()?
                .Trim();

            if (string.IsNullOrWhiteSpace(language))
                return BaseLanguage;

            try
            {
                return CultureInfo
                    .GetCultureInfo(language)
                    .TwoLetterISOLanguageName;
            }
            catch (CultureNotFoundException)
            {
                return BaseLanguage;
            }
        }
    }

    public string GetTwoLetterISOName()
    {
        var culture = CultureInfo.GetCultureInfo(Language);

        var languageCode = culture.TwoLetterISOLanguageName;

        return languageCode;
    }
}