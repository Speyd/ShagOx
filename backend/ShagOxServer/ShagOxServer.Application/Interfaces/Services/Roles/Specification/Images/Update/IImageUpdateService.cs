using ShagOxServer.Application.DTOs.Specification.Images.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Update;
public interface IImageUpdateService
{
    Task<Result<ImageUpdateResponse>> UpdateImageAsync(
       int imageId,
       ImageUpdateRequest request);
}
