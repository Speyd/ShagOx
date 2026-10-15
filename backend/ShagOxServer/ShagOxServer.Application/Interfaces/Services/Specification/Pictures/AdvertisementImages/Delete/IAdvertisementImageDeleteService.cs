using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Delete;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Delete;
public interface IAdvertisementImageDeleteService
    : IPictureDeleteService<AdvertisementImage>
{
}
