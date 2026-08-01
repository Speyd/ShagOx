using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.Application.DTOs.Common.Responses;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Delete;
public interface IAdvertisementDeleteService
{
    Task<Result<DeleteResponse>> DeleteAsync(
        int id);
}