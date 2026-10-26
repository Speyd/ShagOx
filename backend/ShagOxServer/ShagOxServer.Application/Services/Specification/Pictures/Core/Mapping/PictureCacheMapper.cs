using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Domain.Base.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Core.Mapping;
public static class PictureCacheMapper
{
    public static PictureCacheInfo ToInfo(
        BasePicture picture)
    {
        return new PictureCacheInfo(
            picture.Id,
            picture.OwnerId
        );
    }
}
