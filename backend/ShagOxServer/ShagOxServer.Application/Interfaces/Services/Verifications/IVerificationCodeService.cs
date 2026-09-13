namespace ShagOxServer.Application.Interfaces.Services.Verifications;
public interface IVerificationCodeService
{
    Task<string> CreateCodeAsync(int userId);

    Task<bool> VerifyCodeAsync(
        int userId,
        string code);
}