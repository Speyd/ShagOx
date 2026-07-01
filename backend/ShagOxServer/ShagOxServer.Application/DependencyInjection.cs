using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Common.Context;
using ShagOxServer.Application.Common.Validators;
using ShagOxServer.Application.Interfaces.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Advertisements.Delete;
using ShagOxServer.Application.Interfaces.Advertisements.Query;
using ShagOxServer.Application.Interfaces.Advertisements.Update;
using ShagOxServer.Application.Interfaces.Auth;
using ShagOxServer.Application.Interfaces.Common.Context;
using ShagOxServer.Application.Interfaces.Common.Validators;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Delete;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Application.Interfaces.Dictionaries.Categories.Create;
using ShagOxServer.Application.Interfaces.Dictionaries.Categories.Delete;
using ShagOxServer.Application.Interfaces.Dictionaries.Categories.Query;
using ShagOxServer.Application.Interfaces.Dictionaries.Categories.Update;
using ShagOxServer.Application.Interfaces.Jwt;
using ShagOxServer.Application.Interfaces.Location.Cities.Create;
using ShagOxServer.Application.Interfaces.Location.Cities.Delete;
using ShagOxServer.Application.Interfaces.Location.Cities.Query;
using ShagOxServer.Application.Interfaces.Location.Cities.Update;
using ShagOxServer.Application.Interfaces.Location.Regions.Create;
using ShagOxServer.Application.Interfaces.Location.Regions.Delete;
using ShagOxServer.Application.Interfaces.Location.Regions.Query;
using ShagOxServer.Application.Interfaces.Location.Regions.Update;
using ShagOxServer.Application.Interfaces.Roles.Query;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Create;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Delete;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Query;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Update;
using ShagOxServer.Application.Interfaces.Specification.Currencies.Create;
using ShagOxServer.Application.Interfaces.Specification.Currencies.Delete;
using ShagOxServer.Application.Interfaces.Specification.Currencies.Query;
using ShagOxServer.Application.Interfaces.Specification.Currencies.Update;
using ShagOxServer.Application.Interfaces.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Specification.Images.Delete;
using ShagOxServer.Application.Interfaces.Specification.Images.Query;
using ShagOxServer.Application.Interfaces.Specification.Images.Update;
using ShagOxServer.Application.Interfaces.UserRoles;
using ShagOxServer.Application.Interfaces.Users.Delete;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.Application.Interfaces.Users.Update;
using ShagOxServer.Application.Services.Advertisements.Create;
using ShagOxServer.Application.Services.Advertisements.Delete;
using ShagOxServer.Application.Services.Advertisements.Query;
using ShagOxServer.Application.Services.Advertisements.Update;
using ShagOxServer.Application.Services.Auth;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Delete;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Query;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Application.Services.Dictionaries.Categories.Create;
using ShagOxServer.Application.Services.Dictionaries.Categories.Delete;
using ShagOxServer.Application.Services.Dictionaries.Categories.Query;
using ShagOxServer.Application.Services.Dictionaries.Categories.Update;
using ShagOxServer.Application.Services.Location.Cities.Create;
using ShagOxServer.Application.Services.Location.Cities.Delete;
using ShagOxServer.Application.Services.Location.Cities.Query;
using ShagOxServer.Application.Services.Location.Cities.Update;
using ShagOxServer.Application.Services.Location.Regions.Create;
using ShagOxServer.Application.Services.Location.Regions.Delete;
using ShagOxServer.Application.Services.Location.Regions.Query;
using ShagOxServer.Application.Services.Location.Regions.Update;
using ShagOxServer.Application.Services.Roles.Query;
using ShagOxServer.Application.Services.Specification.Conditions.Create;
using ShagOxServer.Application.Services.Specification.Conditions.Delete;
using ShagOxServer.Application.Services.Specification.Conditions.Query;
using ShagOxServer.Application.Services.Specification.Conditions.Update;
using ShagOxServer.Application.Services.Specification.Currencies.Create;
using ShagOxServer.Application.Services.Specification.Currencies.Delete;
using ShagOxServer.Application.Services.Specification.Currencies.Query;
using ShagOxServer.Application.Services.Specification.Currencies.Update;
using ShagOxServer.Application.Services.Specification.Images.Create;
using ShagOxServer.Application.Services.Specification.Images.Delete;
using ShagOxServer.Application.Services.Specification.Images.Query;
using ShagOxServer.Application.Services.Specification.Images.Update;
using ShagOxServer.Application.Services.UserRoles;
using ShagOxServer.Application.Services.Users.Delete;
using ShagOxServer.Application.Services.Users.Query;
using ShagOxServer.Application.Services.Users.Update;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {

        #region Auth
        services.AddScoped<
            IRegisterService,
            RegisterService>();

        services.AddScoped<
            ILoginService,
            LoginService>();
        #endregion

        #region Location

        #region Region
        services.AddScoped<
            IRegionQueryService,
            RegionQueryService>();

        services.AddScoped<
            IRegionCreateService,
            RegionCreateService>();

        services.AddScoped<
            IRegionUpdateService,
            RegionUpdateService>();

        services.AddScoped<
            IRegionDeleteService,
            RegionDeleteService>();
        #endregion

        #region City
        services.AddScoped<
            ICityQueryService,
            CityQueryService>();

        services.AddScoped<
            ICityCreateService,
            CityCreateService>();

        services.AddScoped<
            ICityUpdateService,
            CityUpdateService>();

        services.AddScoped<
            ICityDeleteService,
            CityDeleteService>();
        #endregion

        #endregion

        #region Advertisement
        services.AddScoped<
           IAdvertisementCreateService,
           AdvertisementCreateService>();

        services.AddScoped<
           IAdvertisementDeleteService,
           AdvertisementDeleteService>();

        services.AddScoped<
           IAdvertisementQueryService,
           AdvertisementQueryService>();

        services.AddScoped<
           IAdvertisementUpdateService,
           AdvertisementUpdateService>();
        #endregion

        #region Role
        services.AddScoped<
         IRoleQueryService,
         RoleQueryService>();
        #endregion

        #region User
        services.AddScoped<
          IUserContext,
          UserContext>();

        services.AddScoped<
          IUserQueryService,
          UserQueryService>();

        services.AddScoped<
          IUserAdminQueryService,
          UserAdminQueryService>();

        services.AddScoped<
          IUserUpdateService,
          UserUpdateService>();

        services.AddScoped<
          IUserDeleteService,
          UserDeleteService>();
        #endregion

        #region UserRole
        services.AddScoped<
         IUserRoleService,
         UserRoleService>();
        #endregion

        #region Dictionaries

        #region Category
        services.AddScoped<
           ICategoryQueryService,
           CategoryQueryService>();

        services.AddScoped<
            ICategoryCreateService,
            CategoryCreateService>();

        services.AddScoped<
            ICategoryUpdateService,
            CategoryUpdateService>();

        services.AddScoped<
            ICategoryDeleteService,
            CategoryDeleteService>();
        #endregion

        #region AttributeDefinition
        services.AddScoped<
           IAttributeDefinitionQueryService,
           AttributeDefinitionQueryService>();

        services.AddScoped<
            IAttributeDefinitionCreateService,
            AttributeDefinitionCreateService>();

        services.AddScoped<
            IAttributeDefinitionUpdateService,
            AttributeDefinitionUpdateService>();

        services.AddScoped<
            IAttributeDefinitionDeleteService,
            AttributeDefinitionDeleteService>();
        #endregion

        #endregion

        #region Specification

        #region Currency
        services.AddScoped<
           ICurrencyQueryService,
           CurrencyQueryService>();

        services.AddScoped<
            ICurrencyCreateService,
            CurrencyCreateService>();

        services.AddScoped<
            ICurrencyUpdateService,
            CurrencyUpdateService>();

        services.AddScoped<
            ICurrencyDeleteService,
            CurrencyDeleteService>();
        #endregion

        #region Condition
        services.AddScoped<
           IConditionQueryService,
           ConditionQueryService>();

        services.AddScoped<
            IConditionCreateService,
            ConditionCreateService>();

        services.AddScoped<
            IConditionUpdateService,
            ConditionUpdateService>();

        services.AddScoped<
            IConditionDeleteService,
            ConditionDeleteService>();
        #endregion

        #region Image
        services.AddScoped<
           IImageQueryService,
           ImageQueryService>();

        services.AddScoped<
            IImageCreateService,
            ImageCreateService>();

        services.AddScoped<
            IImageUpdateService,
            ImageUpdateService>();

        services.AddScoped<
            IImageDeleteService,
            ImageDeleteService>();
        #endregion

        #endregion

        #region Common
        services.AddSingleton<
            IContactValidator,
            ContactValidator>();
        #endregion

        #region Settings
        services.AddSingleton<
            IJwtService,
            JwtService>();

        services.AddScoped<
            IPasswordHasher<User>,
            PasswordHasher<User>>();
        #endregion

        return services;
    }
}