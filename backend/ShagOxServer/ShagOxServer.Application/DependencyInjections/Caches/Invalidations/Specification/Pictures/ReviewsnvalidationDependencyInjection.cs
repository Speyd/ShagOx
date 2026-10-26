using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures.Review;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Specification.Pictures;
public static class ReviewsnvalidationDependencyInjection
{
    public static IServiceCollection AddReviewImagesInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ReviewImageInvalidationService>();

        services.AddScoped<ReviewRepliImageInvalidationService>();


        return services;
    }
}
