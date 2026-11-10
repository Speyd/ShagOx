using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Admin.Dashboard;
using ShagOxServer.Infrastructure.Persistence.Repositories.Admin.Dashboard;

namespace ShagOxServer.Infrastructure.DependencyInjections.Admin;
public static class AdminDependencyInjection
{
    public static IServiceCollection AddAdminInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IAdminDashboardRepository, AdminDashboardRepository>();

        return services;
    }
}
