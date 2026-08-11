using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Update;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Delete;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Query;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Update;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Validator;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries;
public static class ProductTypeDependencyInjections
{
    public static IServiceCollection AddProductTypeApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IProductTypeQueryService, ProductTypeQueryService>();
        services.AddScoped<IProductTypeDeleteService, ProductTypeDeleteService>();
        services.AddScoped<IProductTypeUpdateService, ProductTypeUpdateService>();


        services.AddScoped<ProductTypeValidator>();


        return services;
    }
}