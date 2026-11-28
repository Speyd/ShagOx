using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Sending.Sms;
public partial interface ISmsService
{
    Task<Result<bool>> SendResetPasswordCodeAsync(
        string phone,
        string code);

    Task<Result<bool>> SendChangePasswordCodeAsync(
        string phone,
        string code);
}
