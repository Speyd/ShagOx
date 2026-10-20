using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Update;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Update;
public interface IAdvertisementImageUpdateService
    : IPictureUpdateService<AdvertisementImage,
        PictureOrderUpdateRequest>
{
}
