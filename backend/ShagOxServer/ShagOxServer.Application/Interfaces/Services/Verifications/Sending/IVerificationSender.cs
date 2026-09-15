using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
public interface IVerificationSender
{
    Task<Result<string>> SendCode(
        User user,
        VerificationCodePurpose purpose,
        string? pendingValue = null);

    Task<Result<bool>> SendAsync(
        User user,
        VerificationCodePurpose purpose,
        string? pendingValue = null);

    Task<Result<bool>> SendAsync(
        int userId,
        VerificationCodePurpose purpose,
        string? pendingValue = null);
}
