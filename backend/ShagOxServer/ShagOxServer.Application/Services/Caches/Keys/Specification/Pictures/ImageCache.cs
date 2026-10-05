using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Caches.Keys.Specification.Pictures;
public static class ImageCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<Image>();
}