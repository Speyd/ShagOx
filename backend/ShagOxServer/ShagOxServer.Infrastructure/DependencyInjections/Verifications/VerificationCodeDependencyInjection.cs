using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
using ShagOxServer.Infrastructure.Persistence.Repositories.Verifications.VerificationCodes;

namespace ShagOxServer.Infrastructure.DependencyInjections.Verifications;
public static class VerificationCodeDependencyInjection
{
    public static IServiceCollection AddVerificationCodeInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IVerificationCodeQueryRepository, 
            VerificationCodeQueryRepository>();

        services.AddScoped<IVerificationCodeExistsRepository,
            VerificationCodeExistsRepository>();

        return services;
    }
}