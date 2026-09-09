using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Query;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries.Translations;
public static class ProductTypeTranslationDependencyInjections
{
    public static IServiceCollection AddProductTypeTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IProductTypeTranslationQueryService,
            ProductTypeTranslationQueryService>();

        //services.AddScoped<IAttributeDefinitionTranslationCreateService,
        //    AttributeDefinitionTranslationCreateService>();

        //services.AddScoped<IAttributeDefinitionTranslationDeleteService,
        //    AttributeDefinitionTranslationDeleteService>();

        //services.AddScoped<IAttributeDefinitionTranslationUpdateService,
        //    AttributeDefinitionTranslationUpdateService>();

        //services.AddScoped<AttributeDefinitionTranslationValidator>();

        return services;
    }
}