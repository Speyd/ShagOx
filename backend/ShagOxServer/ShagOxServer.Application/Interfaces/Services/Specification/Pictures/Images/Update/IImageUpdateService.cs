using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Update;
public interface IImageUpdateService
    : IUpdateService<UpdateResponse, ImageUpdateRequest>
{
}