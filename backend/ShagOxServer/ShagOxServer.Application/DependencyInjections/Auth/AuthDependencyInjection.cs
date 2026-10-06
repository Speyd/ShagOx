using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Services.Auth;
using ShagOxServer.Application.Services.Auth.Register;

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

        services.AddContactApplication();

        services.AddAuthExternalApplication();

        return services;
    }
}