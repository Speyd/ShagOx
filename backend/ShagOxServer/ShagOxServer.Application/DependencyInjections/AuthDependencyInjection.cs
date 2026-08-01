using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
using ShagOxServer.Application.Services.Auth;
using ShagOxServer.Application.Services.Auth.UserRoles.Query;
using ShagOxServer.Application.Services.Auth.Users.Create;

namespace ShagOxServer.Application.DependencyInjection;
public static class AuthDependencyInjection
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<ILoginService, LoginService>();


        services.AddScoped<UserCreater>();
        services.AddScoped<IUserRoleQueryService, UserRoleQueryService>();


        return services;
    }
}