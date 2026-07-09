using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Jwt;

namespace ShagOxServer.Application.DependencyInjection;

public static class SettingsDependencyInjection
{
    public static IServiceCollection AddSettings(this IServiceCollection services)
    {
        services.AddSingleton<IJwtService, JwtService>();

        return services;
    }
}