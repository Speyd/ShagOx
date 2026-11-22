using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Common.Builders;
using ShagOxServer.Application.Common.Validators;
using ShagOxServer.Application.Interfaces.Services.Common.Validators;
using ShagOxServer.Application.Interfaces.Services.Jwt;

namespace ShagOxServer.Application.DependencyInjection;
public static class CommonDependencyInjection
{
    public static IServiceCollection AddCommonApplication(
        this IServiceCollection services)
    {
        services.AddSingleton<IContactValidator, ContactValidator>();

        services.AddSingleton<IJwtService, JwtService>();

        services.AddSingleton<OrderUrlBuilder>();


        return services;
    }
}
