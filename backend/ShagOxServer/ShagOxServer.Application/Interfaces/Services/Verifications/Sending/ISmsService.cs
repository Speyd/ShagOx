using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
public interface ISmsService
{
    Task<Result<bool>> SendVerificationCodeAsync(
        string phone,
        string code);

    Task<Result<bool>> SendEnableTwoFactorCodeAsync(
        string phone,
        string code);

    Task<Result<bool>> SendDisableTwoFactorCodeAsync(
        string phone,
        string code);

    Task<Result<bool>> SendInitiateTwoFactorCodeAsync(
        string phone,
        string code);
}
