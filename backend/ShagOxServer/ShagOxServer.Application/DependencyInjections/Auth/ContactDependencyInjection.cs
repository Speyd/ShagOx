using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.Emails;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.Passwords;
using ShagOxServer.Application.Services.Auth.Users.Contacts.Emails;
using ShagOxServer.Application.Services.Auth.Users.Contacts.Passwords;

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

        services.AddScoped<IResetPasswordService,
           ResetPasswordService>();

        return services;
    }
}