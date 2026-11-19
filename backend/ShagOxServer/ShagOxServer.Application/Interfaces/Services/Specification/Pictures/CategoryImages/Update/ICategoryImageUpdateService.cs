using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Update;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Update;
public interface ICategoryImageUpdateService
    : IPictureUpdateService<CategoryImage, PictureUpdateRequest>
{
}
