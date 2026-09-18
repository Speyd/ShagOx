using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Verifications.Codes;
using ShagOxServer.Application.Services.Verifications.Codes;

namespace ShagOxServer.Application.DependencyInjections.Verifications;
public static class CodeDependencyInjection
{
    public static IServiceCollection AddVerificationCodeApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IVerificationCodeService, 
            VerificationCodeService>();

        return services;
    }
}