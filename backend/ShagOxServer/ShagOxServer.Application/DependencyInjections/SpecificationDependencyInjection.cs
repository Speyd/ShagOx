using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Update;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Update;
using ShagOxServer.Application.Interfaces.Services.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Images.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Images.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Images.Update;
using ShagOxServer.Application.Services.Specification.Conditions.Create;
using ShagOxServer.Application.Services.Specification.Conditions.Delete;
using ShagOxServer.Application.Services.Specification.Conditions.Query;
using ShagOxServer.Application.Services.Specification.Conditions.Update;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.Application.Services.Specification.Currencies.Create;
using ShagOxServer.Application.Services.Specification.Currencies.Create.Validator;
using ShagOxServer.Application.Services.Specification.Currencies.Delete;
using ShagOxServer.Application.Services.Specification.Currencies.Query;
using ShagOxServer.Application.Services.Specification.Currencies.Update;
using ShagOxServer.Application.Services.Specification.Currencies.Update.Validator;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;
using ShagOxServer.Application.Services.Specification.Images.Create;
using ShagOxServer.Application.Services.Specification.Images.Create.Validator;
using ShagOxServer.Application.Services.Specification.Images.Delete;
using ShagOxServer.Application.Services.Specification.Images.Query;
using ShagOxServer.Application.Services.Specification.Images.Update;
using ShagOxServer.Application.Services.Specification.Images.Validator;

namespace ShagOxServer.Application.DependencyInjection;

public static class SpecificationDependencyInjection
{
    public static IServiceCollection AddSpecification(this IServiceCollection services)
    {
        // Currency
        services.AddScoped<ICurrencyQueryService, CurrencyQueryService>();
        services.AddScoped<ICurrencyCreateService, CurrencyCreateService>();
        services.AddScoped<ICurrencyUpdateService, CurrencyUpdateService>();
        services.AddScoped<ICurrencyDeleteService, CurrencyDeleteService>();

        services.AddScoped<CurrencyCreateValidator>();
        services.AddScoped<CurrencyUpdateValidator>();
        services.AddScoped<CurrencyValidator>();


        // Condition
        services.AddScoped<IConditionQueryService, ConditionQueryService>();
        services.AddScoped<IConditionCreateService, ConditionCreateService>();
        services.AddScoped<IConditionUpdateService, ConditionUpdateService>();
        services.AddScoped<IConditionDeleteService, ConditionDeleteService>();

        services.AddScoped<ConditionValidator>();

        // Image
        services.AddScoped<IImageQueryService, ImageQueryService>();
        services.AddScoped<IImageCreateService, ImageCreateService>();
        services.AddScoped<IImageUpdateService, ImageUpdateService>();
        services.AddScoped<IImageDeleteService, ImageDeleteService>();

        services.AddScoped<ImageCreateValidator>();
        services.AddScoped<ImageValidator>();

        return services;
    }
}