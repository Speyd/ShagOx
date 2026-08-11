using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Specification.Images.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Images.Update;
public interface IImageUpdateService
    : IUpdateService<UpdateResponse, ImageUpdateRequest>
{
}