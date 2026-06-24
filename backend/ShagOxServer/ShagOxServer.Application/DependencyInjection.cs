using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Common.Validators;
using ShagOxServer.Application.Interfaces.Auth;
using ShagOxServer.Application.Interfaces.Jwt;
using ShagOxServer.Application.Interfaces.Validators;
using ShagOxServer.Application.Services.Auth;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {

        services.AddScoped<
            IRegisterService,
            RegisterService>();

        services.AddScoped<
            ILoginService,
            LoginService>();

        services.AddScoped<
            IJwtService,
            JwtService>();

        services.AddScoped<
            IPasswordHasher<User>,
            PasswordHasher<User>>();


        services.AddScoped<
            IContactValidator,
            ContactValidator>();


        return services;
    }
}