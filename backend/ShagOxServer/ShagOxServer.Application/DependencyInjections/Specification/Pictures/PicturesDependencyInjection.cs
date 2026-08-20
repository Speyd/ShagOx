using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Specification.Pictures.Validator;

namespace ShagOxServer.Application.DependencyInjections.Specification.Pictures;
public static class PicturesDependencyInjection
{
    public static IServiceCollection AddPicturesApplication(
        this IServiceCollection services)
    {
        services.AddScoped<PictureValidator>();

        services.AddImageApplication();

        services.AddAvatarApplication();

        return services;
    }
}