using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Advertisements;
using ShagOxServer.Application.DependencyInjections.Auth;
using ShagOxServer.Application.DependencyInjections.Baskets;
using ShagOxServer.Application.DependencyInjections.Caches;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations;
using ShagOxServer.Application.DependencyInjections.Comments;
using ShagOxServer.Application.DependencyInjections.Dashboards;
using ShagOxServer.Application.DependencyInjections.Deliveries;
using ShagOxServer.Application.DependencyInjections.Devices;
using ShagOxServer.Application.DependencyInjections.Dictionary;
using ShagOxServer.Application.DependencyInjections.LineItems;
using ShagOxServer.Application.DependencyInjections.Location;
using ShagOxServer.Application.DependencyInjections.Orders;
using ShagOxServer.Application.DependencyInjections.Payments;
using ShagOxServer.Application.DependencyInjections.Specification;
using ShagOxServer.Application.DependencyInjections.Verifications;

namespace ShagOxServer.Application.DependencyInjections.Base;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        return services
            .AddCacheApplication()
            .AddCacheInvalidationApplication()
            .AddDeviceApplication()
            .AddVerificationApplication()
            .AddAuthApplication()
            .AddBasketApplication()
            .AddLocationApplication()
            .AddCommentsApplication()
            .AddAdvertisementsApplication()
            .AddDictionariesApplication()
            .AddSpecificationApplication()
            .AddCommonApplication()
            .AddLineItemsApplication()
            .AddDeliveriesApplication()
            .AddOrdersItemApplication()
            .AddPaymentsApplication()
            .AddDashboardsApplication();
    }
}
