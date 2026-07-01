using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;
using ShagOxServer.Infrastructure.Interfaces.Auth;
using ShagOxServer.Infrastructure.Interfaces.Auth.Roles;
using ShagOxServer.Infrastructure.Interfaces.Auth.Users;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.AttributeDefinitions;
using ShagOxServer.Infrastructure.Interfaces.Location.Cities;
using ShagOxServer.Infrastructure.Interfaces.Location.Regions;
using ShagOxServer.Infrastructure.Interfaces.Specification;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification;

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
            IUserRoleRepository,
            UserRoleRepository>();
        #endregion

        #region Location

        #region Region
        services.AddScoped<
            IRegionRepository,
            RegionRepository>();
        #endregion

        #region City
        services.AddScoped<
            ICityRepository,
            CityRepository>();
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
            IConditionRepository,
            ConditionRepository>();

        services.AddScoped<
            IImageRepository,
            ImageRepository>();
        #endregion

        #region Dictionaries
        services.AddScoped<
            ICategoryRepository,
            CategoryRepository>();

        services.AddScoped<
            IAttributeDefinitionRepository,
            AttributeDefinitionRepository>();
        #endregion

        return services;
    }
}