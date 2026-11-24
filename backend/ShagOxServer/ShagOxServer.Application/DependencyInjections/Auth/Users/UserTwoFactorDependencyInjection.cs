using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.TwoFactor;
using ShagOxServer.Application.Services.Auth.Users.Contacts.TwoFactor;

namespace ShagOxServer.Application.DependencyInjections.Auth.Users;
public static class UserTwoFactorDependencyInjection
{
    public static IServiceCollection AddUserTwoFactorApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ITwoFactorService, 
            TwoFactorService>();

        return services;
    }
}
