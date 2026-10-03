using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Auth;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Auth;
public static class AuthInvalidationDependencyInjection
{
    public static IServiceCollection AddAuthInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<UserInvalidationService>();

        services.AddScoped<RoleInvalidationService>();


        return services;
    }
}