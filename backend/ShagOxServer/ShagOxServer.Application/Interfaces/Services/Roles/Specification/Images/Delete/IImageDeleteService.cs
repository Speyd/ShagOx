using ShagOxServer.Application.DTOs.Specification.Images.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Delete;
public interface IImageDeleteService
{
    Task<Result<ImageDeleteResponse>> DeleteImageAsync(
        int id);
}
