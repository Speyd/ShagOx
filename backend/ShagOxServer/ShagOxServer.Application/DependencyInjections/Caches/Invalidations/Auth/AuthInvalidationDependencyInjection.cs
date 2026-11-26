using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Auth.Localized;
using ShagOxServer.Application.Services.Caches.Invalidations.Auth;
using ShagOxServer.Application.Services.Caches.Invalidations.Auth.UserInvalid;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Auth;
public static class AuthInvalidationDependencyInjection
{
    public static IServiceCollection AddAuthInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<UserInvalidationService>();

        services.AddScoped<UserDeviceInvalidationService>();

        services.AddScoped<RoleInvalidationService>();

        services.AddAuthLocalizedInvalidationApplication();


        return services;
    }
}
