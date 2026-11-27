using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Update;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Delete;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Create;
using ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Delete;
using ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Query;
using ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Update;
using ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Validator;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.DependencyInjections.Specification.Pictures;
public static class CategoryImageDependencyInjection
{
    public static IServiceCollection AddCategoryImageApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ICategoryImageQueryService,
            CategoryImageQueryService>();

        services.AddScoped<ICategoryImageCreateService,
            CategoryImageCreateService>();

        services.AddScoped<ICategoryImageDeleteService,
            CategoryImageDeleteService>();

        services.AddScoped<ICategoryImageUpdateService,
            CategoryImageUpdateService>();

        services.AddScoped<
           IPictureDeleteService<CategoryImage>,
           CategoryImageDeleteService>();

        services.AddScoped<
            ICacheInvalidationService<CategoryImage, PictureCacheContext>,
            CategoryImageInvalidationService>();

        services.AddScoped<CategoryImageInvalidationService>();

        services.AddScoped<CategoryImageValidator>();

        return services;
    }
}
