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
using ShagOxServer.Application.Interfaces.Jwt;
using ShagOxServer.Application.Interfaces.Users.Delete;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.Application.Interfaces.Users.Update;
using ShagOxServer.Application.Services.Advertisements.Create;
using ShagOxServer.Application.Services.Advertisements.Delete;
using ShagOxServer.Application.Services.Advertisements.Query;
using ShagOxServer.Application.Services.Advertisements.Update;
using ShagOxServer.Application.Services.Auth;
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