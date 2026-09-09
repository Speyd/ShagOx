using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Create;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Delete;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Validator;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries.Translations;
public static class ProductTypeTranslationDependencyInjections
{
    public static IServiceCollection AddProductTypeTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IProductTypeTranslationQueryService,
            ProductTypeTranslationQueryService>();

        services.AddScoped<IProductTypeTranslationCreateService,
            ProductTypeTranslationCreateService>();

        services.AddScoped<IProductTypeTranslationDeleteService,
            ProductTypeTranslationDeleteService>();

        //services.AddScoped<IAttributeDefinitionTranslationUpdateService,
        //    AttributeDefinitionTranslationUpdateService>();

        services.AddScoped<ProductTypeTranslationValidator>();

        return services;
    }
}