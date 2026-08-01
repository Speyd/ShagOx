using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Delete;
public interface IUserDeleteService
{
    Task<Result<DeleteResponse>> DeleteAsync(
        int id);
}