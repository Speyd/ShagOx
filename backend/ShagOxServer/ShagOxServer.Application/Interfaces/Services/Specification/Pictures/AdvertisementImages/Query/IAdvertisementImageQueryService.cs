using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Images;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Query;
public interface IAdvertisementImageQueryService
    : IQueryService<ImageDto, AdvertisementImage, ImageSearchFilter>
{
}
