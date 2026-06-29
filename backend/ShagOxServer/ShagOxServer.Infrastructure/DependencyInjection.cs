using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;
using ShagOxServer.Infrastructure.Interfaces.Auth;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Location.Regions;
using ShagOxServer.Infrastructure.Interfaces.Specification;
using ShagOxServer.Infrastructure.Persistence.Repositories;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries;
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
            IImageRepository,
            ImageRepository>();
        #endregion

        #region Dictionaries
        services.AddScoped<
            ICategoryRepository,
            CategoryRepository>();
        #endregion

        return services;
    }
}