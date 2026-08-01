using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Images.Delete;
public interface IImageDeleteService
{
    Task<Result<DeleteResponse>> DeleteAsync(
        int id);

    Task<Result<DeleteResponse>> DeleteRecordAsync(
        int id);
}