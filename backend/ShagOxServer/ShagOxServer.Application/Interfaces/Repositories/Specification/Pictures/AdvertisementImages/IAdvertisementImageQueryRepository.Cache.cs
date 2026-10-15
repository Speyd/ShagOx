using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Images;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
public partial interface IAdvertisementImageQueryRepository
    : ISearchRepository<AdvertisementImage, ImageSearchFilter>
{
    Task<List<PictureCacheInfo>> GetCacheInfoByAdvertisementAsync(
        long advertId,
        CancellationToken cancellationToken = default);
}
