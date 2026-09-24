using ShagOxServer.Application.Resources.Verifications.Mappers;
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
                    VerificationMapperResources.CodeNotFound);

            case VerificationCodeResult.Expired:
                return Result<bool>.Fail(
                    VerificationMapperResources.CodeExpired);

            case VerificationCodeResult.Invalid:
                return Result<bool>.Fail(
                    VerificationMapperResources.CodeInvalid);

            case VerificationCodeResult.AttemptsExceeded:
                return Result<bool>.Fail(
                    VerificationMapperResources.AttemptsExceeded);

            case VerificationCodeResult.AlreadyUsed:
                return Result<bool>.Fail(
                    VerificationMapperResources.CodeAlreadyUsed);

            case VerificationCodeResult.Success:
                break;

            default:
                return Result<bool>.Fail(
                    VerificationMapperResources.UnknownResult);
        }

        return Result<bool>.Success(true);
    }
}