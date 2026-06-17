using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.Interfaces;
using ShagOxServer.Infrastructure.Persistence.Repositories;

namespace ShagOxServer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {

        services.AddScoped<
            IUserRepository,
            UserRepository>();


        services.AddScoped<
            IRoleRepository,
            RoleRepository>();


        return services;
    }
}