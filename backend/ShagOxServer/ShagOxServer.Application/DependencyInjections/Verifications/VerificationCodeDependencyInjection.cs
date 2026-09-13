using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Verifications;
using ShagOxServer.Application.Services.Verifications;

namespace ShagOxServer.Application.DependencyInjections.Verifications;
public static class VerificationCodeDependencyInjection
{
    public static IServiceCollection AddVerificationCodeApplication(
        this IServiceCollection services)
    {
        services.AddSingleton<IVerificationCodeService, 
            VerificationCodeService>();

        return services;
    }
}