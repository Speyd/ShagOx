using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjection;

namespace ShagOxServer.Application.DependencyInjections.Base;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddAuth()
            .AddLocation()
            .AddAdvertisements()
            .AddUsers()
            .AddRoles()
            .AddDictionaries()
            .AddSpecification()
            .AddCommon()
            .AddSettings();
    }
}