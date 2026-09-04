using ShagOxServer.Api.Providers;
using ShagOxServer.Application.Interfaces.Providers;

namespace ShagOxServer.Api.DependencyInjection;
public static class LanguageExtensions
{
    public static IServiceCollection AddLanguageProvider(
        this IServiceCollection services)
    {
        services.AddLocalization();

        services.AddSingleton<ILanguageProvider, LanguageProvider>();

        return services;
    }
}