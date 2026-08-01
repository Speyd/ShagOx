using ShagOxServer.Application.DTOs.Specification.Images.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.Application.DTOs.Common.Responses;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Images.Update;
public interface IImageUpdateService
{
    Task<Result<UpdateResponse>> UpdateAsync(
       int imageId,
       ImageUpdateRequest request);
}