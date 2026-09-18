using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation;
using ShagOxServer.Application.Services.Verifications.Confirmation;

namespace ShagOxServer.Application.DependencyInjections.Verifications;
public static class ConfirmationDependencyInjection
{
    public static IServiceCollection AddConfirmationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IVerificationService,
            VerificationService>();

        services.AddScoped<IUserVerificationService,
            UserVerificationService>();

        return services;
    }
}