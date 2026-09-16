using ShagOxServer.Application.Services.Verifications.Codes;
using ShagOxServer.Domain.Entities.Verifications.Enum;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Codes;
public interface IVerificationCodeService
{
    Task<string> CreateCodeAsync(
        int userId,
        VerificationCodePurpose purpose,
        string? pendingValue = null);

    Task<VerificationResult> VerifyCodeAsync(
        int userId,
        string code);
}