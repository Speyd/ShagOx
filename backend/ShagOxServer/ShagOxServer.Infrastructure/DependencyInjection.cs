using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;
using ShagOxServer.Infrastructure.Interfaces.Auth;
using ShagOxServer.Infrastructure.Interfaces.Auth.Roles;
using ShagOxServer.Infrastructure.Interfaces.Auth.Users;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.AttributeDefinitions;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.Categories;
using ShagOxServer.Infrastructure.Interfaces.Location.Cities;
using ShagOxServer.Infrastructure.Interfaces.Location.Regions;
using ShagOxServer.Infrastructure.Interfaces.Specification.Conditions;
using ShagOxServer.Infrastructure.Interfaces.Specification.Currencies;
using ShagOxServer.Infrastructure.Interfaces.Specification.Images;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Images;

namespace ShagOxServer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        #region Auth
        services.AddScoped<
            IUserRepository,
            UserRepository>();

        services.AddScoped<
            IUserQueryRepository,
            UserQueryRepository>();

        services.AddScoped<
            IUserExistsRepository,
            UserExistsRepository>();

        services.AddScoped<
            IRoleRepository,
            RoleRepository>();

        services.AddScoped<
           IRoleQueryRepository,
           RoleQueryRepository>();

        services.AddScoped<
           IRoleExistsRepository,
           RoleExistsRepository>();

        services.AddScoped<
            IUserRoleRepository,
            UserRoleRepository>();
        #endregion

        #region Location

        #region Region
        services.AddScoped<
            IRegionRepository,
            RegionRepository>();

        services.AddScoped<
            IRegionQueryRepository,
            RegionQueryRepository>();

        services.AddScoped<
            IRegionExistsRepository,
            Persistence.Repositories.Location.Regions.RegionExistsRepository>();
        #endregion

        #region City
        services.AddScoped<
            ICityRepository,
            CityRepository>();

        services.AddScoped<
            ICityQueryRepository,
            CityQueryRepository>();

        services.AddScoped<
            ICityExistsRepository,
            CityExistsRepository>();
        #endregion

        #endregion

        #region Advertisement
        services.AddScoped<
            IAdvertisementRepository,
            AdvertisementRepository>();
        #endregion

        #region Specification
        services.AddScoped<
            ICurrencyRepository,
            CurrencyRepository>();

        services.AddScoped<
            ICurrencyQueryRepository,
            CurrencyQueryRepository>();

        services.AddScoped<
            ICurrencyExistsRepository,
            CurrencyExistsRepository>();



        services.AddScoped<
            IConditionRepository,
            ConditionRepository>();

        services.AddScoped<
            IConditionQueryRepository,
            ConditionQueryRepository>();

        services.AddScoped<
            IConditionExistsRepository,
            ConditionExistsRepository>();



        services.AddScoped<
            IImageRepository,
            ImageRepository>();

        services.AddScoped<
            IImageQueryRepository,
            ImageQueryRepository>();

        services.AddScoped<
            IImageExistsRepository,
            ImageExistsRepository>();
        #endregion

        #region Dictionaries
        services.AddScoped<
            ICategoryRepository,
            CategoryRepository>();

        services.AddScoped<
            ICategoryQueryRepository,
            CategoryQueryRepository>();

        services.AddScoped<
            ICategoryExistsRepository,
            CategoryExistsRepository>();

        services.AddScoped<
            IAttributeDefinitionRepository,
            AttributeDefinitionRepository>();

        services.AddScoped<
            IAttributeDefinitionQueryRepository,
            AttributeDefinitionQueryRepository>();

        services.AddScoped<
            IAttributeDefinitionExistsRepository,
            AttributeDefinitionExistsRepository>();
        #endregion

        return services;
    }
}