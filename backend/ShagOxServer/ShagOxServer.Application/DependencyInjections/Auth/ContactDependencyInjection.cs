using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.Emails;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.Passwords;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.Phones;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.UserNames;
using ShagOxServer.Application.Services.Auth.Users.Contacts;
using ShagOxServer.Application.Services.Auth.Users.Contacts.Emails;
using ShagOxServer.Application.Services.Auth.Users.Contacts.Passwords;
using ShagOxServer.Application.Services.Auth.Users.Contacts.Phones;
using ShagOxServer.Application.Services.Auth.Users.Contacts.UserNames;

namespace ShagOxServer.Application.DependencyInjections.Auth;
public static class ContactDependencyInjection
{
    public static IServiceCollection AddContactApplication(
        this IServiceCollection services)
    {
        services.AddScoped<UserContactApplier>();

        #region Email
        services.AddScoped<IChangeEmailService,
           ChangeEmailService>();
        #endregion

        #region Phone
        services.AddScoped<IChangePhoneService,
           ChangePhoneService>();
        #endregion

        #region UserName
        services.AddScoped<IUserNameService,
           UserNameService>();
        #endregion

        #region Password
        services.AddScoped<UserPasswordService>();

        services.AddScoped<IChangePasswordService,
           ChangePasswordService>();

        services.AddScoped<IResetPasswordService,
           ResetPasswordService>();
        #endregion 

        return services;
    }
}