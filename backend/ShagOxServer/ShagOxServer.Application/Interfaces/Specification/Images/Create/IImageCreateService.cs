using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Specification.Images.Create;
public interface IImageCreateService
{
    Task<Result<ImageCreateResponse>> CreateImageAsync(
       ImageCreateRequest request);
}
