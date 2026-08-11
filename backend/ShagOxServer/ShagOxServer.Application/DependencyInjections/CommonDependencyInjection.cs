using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Common.Validators;
using ShagOxServer.Application.Interfaces.Services.Common.Validators;

namespace ShagOxServer.Application.DependencyInjection;
public static class CommonDependencyInjection
{
    public static IServiceCollection AddCommonApplication(
        this IServiceCollection services)
    {
        services.AddSingleton<IContactValidator, ContactValidator>();

        return services;
    }
}