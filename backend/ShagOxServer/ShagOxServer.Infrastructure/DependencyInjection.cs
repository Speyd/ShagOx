using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.Interfaces;
using ShagOxServer.Infrastructure.Interfaces.Auth;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Specification;
using ShagOxServer.Infrastructure.Persistence.Repositories;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries;
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
        #endregion

        #region Dictionaries
        services.AddScoped<
            ICategoryRepository,
            CategoryRepository>();
        #endregion

        return services;
    }
}