using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Dictionaries.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Create;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Delete;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Query;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Update;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Validator;
using ShagOxServer.Domain.Cache.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries;
public static class ProductTypeDependencyInjections
{
    public static IServiceCollection AddProductTypeApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IProductTypeQueryService,
            ProductTypeQueryService>();

        services.AddScoped<IProductTypeCreateService,
            ProductTypeCreateService>();

        services.AddScoped<IProductTypeDeleteService,
            ProductTypeDeleteService>();

        services.AddScoped<IProductTypeUpdateService,
            ProductTypeUpdateService>();

        services.AddScoped<ProductTypeValidator>();

        services.AddScoped<
            ICacheInvalidationService<ProductType, ProductTypeCacheContext>,
            ProductTypeInvalidationService>();

        services.AddScoped<ProductTypeInvalidationService>();

        services.AddProductTypeTranslationApplication();

        return services;
    }
}
