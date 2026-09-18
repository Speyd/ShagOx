using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Services.Verifications.Sending;

namespace ShagOxServer.Application.DependencyInjections.Verifications;
public static class SendingDependencyInjection
{
    public static IServiceCollection AddSendingApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IEmailService,
            EmailService>();

        services.AddScoped<ISmsService,
            SmsService>();

        services.AddScoped<IVerificationSender,
           VerificationSender>();

        return services;
    }
}