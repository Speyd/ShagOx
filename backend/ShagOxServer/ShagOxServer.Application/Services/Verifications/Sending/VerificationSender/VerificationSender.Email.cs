using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Sending.VerificationSender;
public partial class VerificationSender
{
    private async Task<Result<bool>> SendCodeEmail(
        VerificationCodePurpose purpose,
        string? email,
        string code,
        string? pendingValue = null)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result<bool>.Fail(
                VerificationResources.NoUserEmail);
        }

        return purpose switch
        {
            VerificationCodePurpose.ChangeNewEmail
               when pendingValue is not null =>
               await _emailService.SendNewEmailConfirmationCodeAsync(
                   pendingValue, code),

            VerificationCodePurpose.ChangeCurrentEmail
                when email is not null && pendingValue is not null =>
                await _emailService.SendEmailChangeApprovalCodeAsync(
                    email, pendingValue, code),

            VerificationCodePurpose.EnableTwoFactorEmail =>
                await _emailService.SendEnableTwoFactorCodeAsync(
                    email, code),

            VerificationCodePurpose.DisableTwoFactorEmail =>
                await _emailService.SendDisableTwoFactorCodeAsync(
                    email, code),

            VerificationCodePurpose.InitiateTwoFactorEmail =>
                await _emailService.SendInitiateTwoFactorCodeAsync(
                    email, code),

            VerificationCodePurpose.ResetPasswordEmail =>
                await _emailService.SendResetPasswordCodeAsync(
                    email, code),

            VerificationCodePurpose.ChangePasswordEmail =>
                await _emailService.SendChangePasswordCodeAsync(
                    email, code),

            VerificationCodePurpose.RegistrationEmail
               => await _emailService.SendVerificationCodeAsync(
                   email, code),

            _ => Result<bool>.Fail(
                VerificationResources.UnsupportedVerificationPurpose)
        };
    }
}
