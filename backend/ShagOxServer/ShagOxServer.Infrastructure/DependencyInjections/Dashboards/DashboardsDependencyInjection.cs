using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Dashboards;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin;

namespace ShagOxServer.Infrastructure.DependencyInjections.Dashboards;
public static class DashboardsDependencyInjection
{
    public static IServiceCollection AddDashboardsInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IDashboardAdminRepository,
            DashboardAdminRepository>();


        return services;
    }
}
