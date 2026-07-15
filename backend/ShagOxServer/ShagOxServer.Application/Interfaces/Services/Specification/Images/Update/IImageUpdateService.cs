using ShagOxServer.Application.DTOs.Specification.Images.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Images.Update;
public interface IImageUpdateService
{
    Task<Result<ImageUpdateResponse>> UpdateAsync(
       int imageId,
       ImageUpdateRequest request);
}