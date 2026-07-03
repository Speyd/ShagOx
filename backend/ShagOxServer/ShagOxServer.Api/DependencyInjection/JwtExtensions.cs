using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ShagOxServer.Application.Common.Settings;
using System.Text;

namespace ShagOxServer.Api.DependencyInjection;
public static class JwtExtensions
{
    public static IServiceCollection AddJWT(
        this IServiceCollection services,
        IConfiguration config)
    {
        var jwtSettings = config.GetSection("Jwt").Get<JwtSettings>()
            ?? throw new Exception("Jwt settings not found");

        services.Configure<JwtSettings>(
            config.GetSection("Jwt"));

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Key))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token =
                            context.Request.Cookies["access_token"];

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization();

        return services;
    }
}