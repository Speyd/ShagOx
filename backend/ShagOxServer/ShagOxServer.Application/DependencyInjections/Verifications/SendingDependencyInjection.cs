using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Services.Verifications.Sending.Email;
using ShagOxServer.Application.Services.Verifications.Sending.Email.Processing;
using ShagOxServer.Application.Services.Verifications.Sending.Phone;
using ShagOxServer.Application.Services.Verifications.Sending.VerificationSender;

namespace ShagOxServer.Application.DependencyInjections.Verifications;
public static class SendingDependencyInjection
{
    public static IServiceCollection AddSendingApplication(
        this IServiceCollection services)
    {
        #region Email

        services.AddScoped<IEmailService,
            EmailService>();

        services.AddSingleton<EmailTemplateRenderer>();

        #endregion


        services.AddScoped<ISmsService,
            SmsService>();

        services.AddScoped<IVerificationSender,
           VerificationSender>();

        return services;
    }
}
