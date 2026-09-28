using ShagOxServer.Application.Services.Verifications.Codes;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Codes;
public interface IVerificationCodeService
{
    Task<Result<string>> CreateCodeAsync(
        long userId,
        VerificationCodePurpose purpose,
        string? pendingValue = null);

    Task<VerificationResult> VerifyCodeAsync(
        long userId,
        string code);
}