using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Domain.Base.Pictures;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core.Single.Extensions;
public static class PictureQueryExtensions
{
    public static PictureCacheInfo SelectCacheInfo(
        this BasePicture x)
    {
        return new PictureCacheInfo(
            x.Id,
            x.OwnerId);
    }
}
