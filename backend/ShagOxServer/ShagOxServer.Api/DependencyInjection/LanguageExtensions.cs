using Microsoft.AspNetCore.Localization;
using ShagOxServer.Api.Providers;
using ShagOxServer.Application.Interfaces.Providers;
using System.Globalization;

namespace ShagOxServer.Api.DependencyInjection;
public static class LanguageExtensions
{
    public static IServiceCollection AddLanguageProvider(
        this IServiceCollection services)
    {
        var supportedCultures = new[]
        {
            new CultureInfo("en"),
            new CultureInfo("ru"),
            new CultureInfo("uk"),
            new CultureInfo("de")
        };

        services.AddLocalization(options =>
        {
            options.ResourcesPath = "Resources";
        });

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture("en");

            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });



        services.AddSingleton<ILanguageProvider, LanguageProvider>();

        return services;
    }
}