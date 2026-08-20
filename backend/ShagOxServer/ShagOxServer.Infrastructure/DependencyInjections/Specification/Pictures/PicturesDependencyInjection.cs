using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Specification.Pictures;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Infrastructure.DependencyInjections.Specification.Pictures;
public static class PicturesDependencyInjection
{
    public static IServiceCollection AddPicturesInfrastructure(
       this IServiceCollection services)
    {
        services.AddImageInfrastructure();

        services.AddAvatarInfrastructure();

        return services;
    }
}
