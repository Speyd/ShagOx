using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Services.Auth;

namespace ShagOxServer.Application.DependencyInjection;
public static class AuthDependencyInjection
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<ILoginService, LoginService>();


        services.AddScoped<UserCreater>();

        return services;
    }
}