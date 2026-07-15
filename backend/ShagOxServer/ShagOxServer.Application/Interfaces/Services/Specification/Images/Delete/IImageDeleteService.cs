using ShagOxServer.Application.DTOs.Specification.Images.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Images.Delete;
public interface IImageDeleteService
{
    Task<Result<ImageDeleteResponse>> DeleteAsync(
        int id);

    Task<Result<ImageDeleteResponse>> DeleteRecordAsync(
        int id);
}