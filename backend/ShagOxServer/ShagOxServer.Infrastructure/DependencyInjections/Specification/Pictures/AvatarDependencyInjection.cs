using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Avatars;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Specification.Pictures;
public static class AvatarDependencyInjection
{
    public static IServiceCollection AddAvatarInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IAvatarQueryRepository, AvatarQueryRepository>();

        return services;
    }
}