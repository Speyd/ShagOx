using ShagOxServer.Application.DTOs.Auth.Users.Contacts.Passwords;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts;
public interface IChangePasswordService
{
    Task<Result<bool>> ChangePassword(
        int userId,
        ChangePasswordRequest request);
}