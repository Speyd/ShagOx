using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth.Core.Login;
using ShagOxServer.Application.Services.Auth.Core.Login;

namespace ShagOxServer.Application.DependencyInjections.Auth;
public static class LoginDependencyInjection
{
    public static IServiceCollection AddLoginApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ILoginService,
            LoginService>();

        services.AddScoped<ILoginPolicy,
            LoginPolicy>();

        services.AddScoped<ILoginSessionService,
            LoginSessionService>();

        return services;
    }
}
