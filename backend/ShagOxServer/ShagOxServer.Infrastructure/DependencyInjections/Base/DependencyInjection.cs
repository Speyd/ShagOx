using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.DependencyInjections.Advertisement;
using ShagOxServer.Infrastructure.DependencyInjections.Auth;
using ShagOxServer.Infrastructure.DependencyInjections.Dictionaries;
using ShagOxServer.Infrastructure.DependencyInjections.Location;
using ShagOxServer.Infrastructure.DependencyInjections.Specification;

namespace ShagOxServer.Infrastructure.DependencyInjections.Base;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        return services
            .AddBaseRepositories()
            .AddAuthInfrastructure()
            .AddLocationInfrastructure()
            .AddAdvertisementInfrastructure()
            .AddUnitOfWorkInfrastructure()
            .AddSpecificationInfrastructure()
            .AddDictionariesInfrastructure()
            .AddCloudinary();
    }
}