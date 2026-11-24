using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Common.Builders;
using ShagOxServer.Application.Common.Validators;
using ShagOxServer.Application.Interfaces.Services.Auth.Tokens.Jwt;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Services.Common.Validators;
using ShagOxServer.Application.Services.Auth.Tokens.RefreshToken;

namespace ShagOxServer.Application.DependencyInjection;
public static class CommonDependencyInjection
{
    public static IServiceCollection AddCommonApplication(
        this IServiceCollection services)
    {
        services.AddSingleton<IContactValidator,
            ContactValidator>();

        services.AddSingleton<IJwtService, JwtService>();

        services.AddSingleton<IRefreshTokenService, 
            RefreshTokenService>();

        services.AddSingleton<OrderUrlBuilder>();


        return services;
    }
}
