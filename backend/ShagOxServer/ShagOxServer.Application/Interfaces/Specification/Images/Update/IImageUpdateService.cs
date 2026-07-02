using ShagOxServer.SharedKernel.Results;
using ShagOxServer.Application.DTOs.Specification.Images.Update;

namespace ShagOxServer.Application.Interfaces.Specification.Images.Update;
public interface IImageUpdateService
{
    Task<Result<ImageUpdateResponse>> UpdateImageAsync(
       int imageId,
       ImageUpdateRequest request);
}
