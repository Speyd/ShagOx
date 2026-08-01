using ShagOxServer.Application.DTOs.Auth.Users.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.Application.DTOs.Common.Responses;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Update;
public interface IUserUpdateService
{
    Task<Result<UpdateResponse>> UpdateAsync(
        int userId,
        UserUpdateRequest request);
}