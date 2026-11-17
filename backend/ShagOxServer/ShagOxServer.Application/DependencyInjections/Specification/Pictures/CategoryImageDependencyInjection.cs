using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Query;
using ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Query;

namespace ShagOxServer.Application.DependencyInjections.Specification.Pictures;
public static class CategoryImageDependencyInjection
{
    public static IServiceCollection AddCategoryImageApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ICategoryImageQueryService,
            CategoryImageQueryService>();
        //services.AddScoped<IAvatarCreateService, AvatarCreateService>();
        //services.AddScoped<IAvatarDeleteService, AvatarDeleteService>();
        //services.AddScoped<IAvatarUpdateService, AvatarUpdateService>();

        //services.AddScoped<
        //   IPictureDeleteService<Avatar>,
        //   AvatarDeleteService>();

        //services.AddScoped<AvatarValidator>();

        return services;
    }
}
