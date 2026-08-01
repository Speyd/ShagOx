using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Location.Cities.Delete;
public interface ICityDeleteService
{
    Task<Result<DeleteResponse>> DeleteAsync(
       int id);
}