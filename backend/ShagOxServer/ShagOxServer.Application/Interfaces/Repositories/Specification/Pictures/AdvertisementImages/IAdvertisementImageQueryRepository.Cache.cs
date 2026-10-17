using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.AdvertisementImages;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
public partial interface IAdvertisementImageQueryRepository
    : ISearchRepository<AdvertisementImage, AdvertisementImageSearchFilter>,
    IPictureOrderQueryRepository<AdvertisementImage>
{
}
