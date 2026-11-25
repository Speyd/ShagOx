using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dashboards;
using ShagOxServer.Application.Services.Dashboards;

namespace ShagOxServer.Application.DependencyInjections.Dashboards;
public static class DashboardsDependencyInjections
{
    public static IServiceCollection AddDashboardsApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IDashboardAdminService,
            DashboardAdminService>();

        return services;
    }
}
