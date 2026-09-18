using ShagOxServer.Application.DTOs.Auth.Users.Contacts.Emails;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.Emails;
public interface IChangeEmailService
{
    Task<Result<bool>> ChangeEmail(
        int userId,
        ChangeEmailRequest request);
}