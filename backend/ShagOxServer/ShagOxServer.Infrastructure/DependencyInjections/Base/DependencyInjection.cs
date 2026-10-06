using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.DependencyInjections.Advertisements;
using ShagOxServer.Infrastructure.DependencyInjections.Auth;
using ShagOxServer.Infrastructure.DependencyInjections.BackgroundServices;
using ShagOxServer.Infrastructure.DependencyInjections.Baskets;
using ShagOxServer.Infrastructure.DependencyInjections.Dictionaries;
using ShagOxServer.Infrastructure.DependencyInjections.Location;
using ShagOxServer.Infrastructure.DependencyInjections.Specification;
using ShagOxServer.Infrastructure.DependencyInjections.Verifications;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;

namespace ShagOxServer.Infrastructure.DependencyInjections.Base;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<BaseAppDbContext>(
    provider =>
        provider.GetRequiredService<ReplicaDbContext>());
        return services
            .AddVerificationInfrastructure()
            .AddBackgroundServiceInfrastructure()
            .AddBaseRepositoryInfrastructure()
            .AddBasketInfrastructure()
            .AddAuthInfrastructure()
            .AddLocationInfrastructure()
            .AddAdvertisementsInfrastructure()
            .AddUnitOfWorkInfrastructure()
            .AddSpecificationInfrastructure()
            .AddDictionariesInfrastructure()
            .AddCloudinary();
    }
}