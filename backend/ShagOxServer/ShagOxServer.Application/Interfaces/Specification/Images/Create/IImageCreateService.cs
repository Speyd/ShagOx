using ShagOxServer.SharedKernel.Results;
using ShagOxServer.Application.DTOs.Specification.Images.Create;

namespace ShagOxServer.Application.Interfaces.Specification.Images.Create;
public interface IImageCreateService
{
    Task<Result<ImageCreateResponse>> CreateImageAsync(
       ImageCreateRequest request);
}
