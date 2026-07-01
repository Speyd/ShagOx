using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Specification.Images.Update;

namespace ShagOxServer.Application.Interfaces.Specification.Images.Update;
public interface IImageQueryService
{
    Task<Result<ImageUpdateResponse>> UpdateImageAsync(
       int imageId,
       ImageUpdateRequest request);
}
