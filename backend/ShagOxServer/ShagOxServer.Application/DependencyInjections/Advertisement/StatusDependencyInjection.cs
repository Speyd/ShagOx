using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
using ShagOxServer.Application.Services.Advertisements.Statuses.Create;
using ShagOxServer.Application.Services.Advertisements.Statuses.Delete;
using ShagOxServer.Application.Services.Advertisements.Statuses.Query;
using ShagOxServer.Application.Services.Advertisements.Statuses.Validator;

namespace ShagOxServer.Application.DependencyInjections.Advertisement;
public static class StatusDependencyInjection
{
    public static IServiceCollection AddStatusApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IStatusQueryService, StatusQueryService>();
        services.AddScoped<IStatusCreateService, StatusCreateService>();
        services.AddScoped<IStatusDeleteService, StatusDeleteService>();

        services.AddScoped<StatusValidator>();

        return services;
    }
}