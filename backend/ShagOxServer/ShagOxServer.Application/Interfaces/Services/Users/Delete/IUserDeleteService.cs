using ShagOxServer.Application.DTOs.Users.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Users.Delete;
public interface IUserDeleteService
{
    Task<Result<UserDeleteResponse>> DeleteAsync(
        int id);
}