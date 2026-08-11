using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Update;
using ShagOxServer.Application.Services.Specification.Conditions.Delete;
using ShagOxServer.Application.Services.Specification.Conditions.Query;
using ShagOxServer.Application.Services.Specification.Conditions.Update;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;

namespace ShagOxServer.Application.DependencyInjections.Specification;
public static class ConditionDependencyInjection
{
    public static IServiceCollection AddConditionApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IConditionQueryService, ConditionQueryService>();
        services.AddScoped<IConditionUpdateService, ConditionUpdateService>();
        services.AddScoped<IConditionDeleteService, ConditionDeleteService>();

        services.AddScoped<ConditionValidator>();

        return services;
    }
}