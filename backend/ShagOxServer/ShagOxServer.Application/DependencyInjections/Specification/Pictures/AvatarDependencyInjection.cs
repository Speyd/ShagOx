using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Update;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Delete;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Create;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Delete;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Update;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.DependencyInjections.Specification.Pictures;
public static class AvatarDependencyInjection
{
    public static IServiceCollection AddAvatarApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAvatarQueryService, AvatarQueryService>();
        services.AddScoped<IAvatarCreateService, AvatarCreateService>();
        services.AddScoped<IAvatarDeleteService, AvatarDeleteService>();
        services.AddScoped<IAvatarUpdateService, AvatarUpdateService>();

        services.AddScoped<
           IPictureDeleteService<Avatar>,
           AvatarDeleteService>();

        services.AddScoped<AvatarValidator>();

        return services;
    }
}
