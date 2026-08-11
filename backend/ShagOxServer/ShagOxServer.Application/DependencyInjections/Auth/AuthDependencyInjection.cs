using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Services.Auth;

namespace ShagOxServer.Application.DependencyInjections.Auth;
public static class AuthDependencyInjection
{
    public static IServiceCollection AddAuthApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<ILoginService, LoginService>();

        services.AddUsersApplication();

        services.AddRoleApplication();

        return services;
    }
}