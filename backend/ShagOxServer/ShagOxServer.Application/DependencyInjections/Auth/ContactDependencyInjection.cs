using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts;
using ShagOxServer.Application.Services.Auth.Users.Contacts;

namespace ShagOxServer.Application.DependencyInjections.Auth;
public static class ContactDependencyInjection
{
    public static IServiceCollection AddContactApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IChangeEmailService,
            ChangeEmailService>();

        services.AddScoped<IChangePasswordService,
           ChangePasswordService>();

        return services;
    }
}