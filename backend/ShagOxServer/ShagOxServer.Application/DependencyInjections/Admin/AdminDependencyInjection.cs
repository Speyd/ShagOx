using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Admin.Dashboard;
using ShagOxServer.Application.Services.Admin.Dashboard;

namespace ShagOxServer.Application.DependencyInjections.Admin;
public static class AdminDependencyInjection
{
    public static IServiceCollection AddAdminApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAdminDashboardService, AdminDashboardService>();

        return services;
    }
}
