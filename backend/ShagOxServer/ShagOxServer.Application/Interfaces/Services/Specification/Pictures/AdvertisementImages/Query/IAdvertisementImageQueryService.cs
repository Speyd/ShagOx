using ShagOxServer.Application.DTOs.Specification.Pictures.AdvertisementImages.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.AdvertisementImages;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Query;
public interface IAdvertisementImageQueryService
    : IQueryService<AdvertisementImageDto,
        AdvertisementImage,
        AdvertisementImageSearchFilter>
{
}
