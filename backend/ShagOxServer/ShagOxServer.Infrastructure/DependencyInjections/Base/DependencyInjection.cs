using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.Base;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddBaseRepositories()
            .AddAuthInfrastructure()
            .AddLocationInfrastructure()
            .AddAdvertisementsInfrastructure()
            .AddUnitOfWorkInfrastructure()
            .AddSpecificationInfrastructure()
            .AddDictionariesInfrastructure()
            .AddCloudinary();
    }
}