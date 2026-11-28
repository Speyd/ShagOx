using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Sending.Email;
public partial interface IEmailService
{
    Task<Result<bool>> SendChangePasswordCodeAsync(
        string email,
        string code);

    Task<Result<bool>> SendResetPasswordCodeAsync(
        string email,
        string code);
}
