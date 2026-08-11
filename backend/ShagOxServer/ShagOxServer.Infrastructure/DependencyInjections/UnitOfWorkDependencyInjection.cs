using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Infrastructure.Persistence;

namespace ShagOxServer.Infrastructure.DependencyInjections;
public static class UnitOfWorkDependencyInjection
{
    public static IServiceCollection AddUnitOfWorkInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}