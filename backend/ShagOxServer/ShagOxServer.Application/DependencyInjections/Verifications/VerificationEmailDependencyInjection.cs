using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Verifications;
using ShagOxServer.Application.Services.Verifications;

namespace ShagOxServer.Application.DependencyInjections.Verifications;
public static class VerificationEmailDependencyInjection
{
    public static IServiceCollection AddVerificationEmailApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IEmailService,
            EmailService>();

        services.AddScoped<IVerificationEmailService,
            VerificationEmailService>();

        return services;
    }
}