using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth.Externals;
using ShagOxServer.Application.Services.Auth.Externals;
namespace ShagOxServer.Application.DependencyInjections.Auth;
public static class AuthExternalDependencyInjection
{
    public static IServiceCollection AddAuthExternalApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IGoogleLoginService,
            GoogleLoginService>();

        return services;
    }
}