using ShagOxServer.Application.Services.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Mappers;
public static class VerificationResultMapper
{
    public static Result<bool> MapVerificationResult(
        VerificationCodeResult result)
    {
        switch (result)
        {
            case VerificationCodeResult.NotFound:
                return Result<bool>.Fail(
                    "Verification code not found.");

            case VerificationCodeResult.Expired:
                return Result<bool>.Fail(
                    "Verification code has expired.");

            case VerificationCodeResult.Invalid:
                return Result<bool>.Fail(
                    "Invalid verification code.");

            case VerificationCodeResult.AttemptsExceeded:
                return Result<bool>.Fail(
                    "Too many attempts.");

            case VerificationCodeResult.AlreadyUsed:
                return Result<bool>.Fail(
                    "Verification code has already been used.");

            case VerificationCodeResult.Success:
                break;

            default:
                return Result<bool>.Fail(
                    "Unknown verification result.");
        }

        return Result<bool>.Success(true);
    }
}