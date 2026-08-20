using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Create;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Query;

namespace ShagOxServer.Application.DependencyInjections.Specification.Pictures;
public static class AvatarDependencyInjection
{
    public static IServiceCollection AddAvatarApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAvatarQueryService, AvatarQueryService>();
        services.AddScoped<IAvatarCreateService, AvatarCreateService>();

        return services;
    }
}