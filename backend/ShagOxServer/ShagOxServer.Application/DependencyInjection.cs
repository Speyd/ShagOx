using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces;
using ShagOxServer.Application.Services;
using ShagOxServer.Application.Validators;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {

        services.AddScoped<
            IAuthService,
            AuthService>();


        services.AddScoped<
            IPasswordHasher<User>,
            PasswordHasher<User>>();


        services.AddScoped<
            IContactValidator,
            ContactValidator>();


        return services;
    }
}