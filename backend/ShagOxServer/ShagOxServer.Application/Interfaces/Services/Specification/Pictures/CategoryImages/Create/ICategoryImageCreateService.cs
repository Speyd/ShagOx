using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Create;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Create;
public interface ICategoryImageCreateService
    : IPictureCreateService<CategoryImage, PictureCreateRequest>
{
}
