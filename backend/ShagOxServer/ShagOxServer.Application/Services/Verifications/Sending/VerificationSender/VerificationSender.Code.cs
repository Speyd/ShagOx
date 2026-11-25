using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Sending.VerificationSender;
public partial class VerificationSender
{
    private async Task<Result<string>> SendCode(
        User user,
        VerificationCodePurpose purpose,
        string? pendingValue)
    {
        ApplyPendingStatus(user, purpose);

        var code = await _codeService.CreateCodeAsync(
            user.Id,
            purpose,
            pendingValue);

        if (!code.IsSuccess || code.Value is null)
        {
            return Result<string>.Fail(
                VerificationResources.FailedToGenerateVerificationCode);
        }

        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Success(code.Value);
    }

    private static void ApplyPendingStatus(
        User user,
        VerificationCodePurpose purpose)
    {
        if (purpose is VerificationCodePurpose.RegistrationEmail
            or VerificationCodePurpose.RegistrationPhone)
        {
            user.Status = UserStatus.PendingVerification;
            user.EmailConfirmed = purpose ==
                VerificationCodePurpose.RegistrationEmail
                ? false
                : user.EmailConfirmed;

            user.PhoneConfirmed = purpose ==
                VerificationCodePurpose.RegistrationPhone
                ? false
                : user.PhoneConfirmed;
        }
    }

    private static void ApplyConfirmationStatus(
        User user,
        VerificationCodePurpose purpose)
    {
        switch (purpose)
        {
            case VerificationCodePurpose.RegistrationEmail:
                user.EmailConfirmed = false;
                break;

            case VerificationCodePurpose.RegistrationPhone:
                user.PhoneConfirmed = false;
                break;
        }
    }
}
