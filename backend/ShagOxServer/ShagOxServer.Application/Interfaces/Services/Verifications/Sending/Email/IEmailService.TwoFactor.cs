using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Sending.Email;
public partial interface IEmailService
{
    Task<Result<bool>> SendEnableTwoFactorCodeAsync(
        string email,
        string code);

    Task<Result<bool>> SendDisableTwoFactorCodeAsync(
        string email,
        string code);

    Task<Result<bool>> SendInitiateTwoFactorCodeAsync(
        string email,
        string code);
}
