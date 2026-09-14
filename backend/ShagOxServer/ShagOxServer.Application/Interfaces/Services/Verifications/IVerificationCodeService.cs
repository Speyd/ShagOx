using ShagOxServer.Application.Services.Verifications.Enum;

namespace ShagOxServer.Application.Interfaces.Services.Verifications;
public interface IVerificationCodeService
{
    Task<string> CreateCodeAsync(int userId);

    Task<VerificationCodeResult> VerifyCodeAsync(
        int userId,
        string code);
}