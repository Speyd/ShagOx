using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Create;
using ShagOxServer.Application.Services.Specification.Pictures.PictureOrdering;
using ShagOxServer.Application.Services.Specification.Pictures.Validator;

namespace ShagOxServer.Application.DependencyInjections.Specification.Pictures;
public static class PicturesDependencyInjection
{
    public static IServiceCollection AddPicturesApplication(
        this IServiceCollection services)
    {
        services.AddScoped<PictureValidator>();

        services.AddScoped(
            typeof(IPictureOrderingService<,>),
            typeof(PictureOrderingService<,>));

        //services.AddScoped(
        //    typeof(IPictureCreateService<,>),
        //    typeof(PictureOrderingService<,>));

        services.AddImageApplication();

        services.AddAvatarApplication();

        return services;
    }
}
