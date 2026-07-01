using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Specification.Images.Delete;

namespace ShagOxServer.Application.Interfaces.Specification.Images.Delete;
public interface IImageDeleteService
{
    Task<Result<ImageDeleteResponse>> DeleteImageAsync(
        int id);
}
