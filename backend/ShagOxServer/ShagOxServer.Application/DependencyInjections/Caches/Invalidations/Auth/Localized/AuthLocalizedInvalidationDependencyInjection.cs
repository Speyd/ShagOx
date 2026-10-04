using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Auth.Localized;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Auth.Localized;
public static class AuthLocalizedInvalidationDependencyInjection
{
    public static IServiceCollection AddAuthLocalizedInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<UserLocalizedInvalidationService>();


        return services;
    }
}