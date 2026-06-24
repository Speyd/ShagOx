using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.Interfaces;
using ShagOxServer.Infrastructure.Interfaces.Auth;
using ShagOxServer.Infrastructure.Persistence.Repositories;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth;

namespace ShagOxServer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        //---------Auth-----------
        services.AddScoped<
            IUserRepository,
            UserRepository>();


        services.AddScoped<
            IRoleRepository,
            RoleRepository>();

        //---------Advertisement-----------
        services.AddScoped<
            IAdvertisementRepository,
            AdvertisementRepository>();


        return services;
    }
}