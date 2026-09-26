using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Delete;
public interface IImageDeleteService
{
    Task<Result<DeleteResponse>> DeleteAsync(
        long id);

    Task<Result<DeleteResponse>> DeleteRecordAsync(
        long id);
}