using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Advertisement;
using ShagOxServer.Application.DependencyInjections.Auth;
using ShagOxServer.Application.DependencyInjections.Baskets;
using ShagOxServer.Application.DependencyInjections.Dictionary;
using ShagOxServer.Application.DependencyInjections.Location;
using ShagOxServer.Application.DependencyInjections.Specification;

namespace ShagOxServer.Application.DependencyInjections.Base;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        return services
            .AddAuthApplication()
            .AddBasketApplication()
            .AddLocationApplication()
            .AddAdvertisementApplication()
            .AddDictionariesApplication()
            .AddSpecificationApplication()
            .AddCommonApplication()
            .AddSettingsApplication();
    }
}