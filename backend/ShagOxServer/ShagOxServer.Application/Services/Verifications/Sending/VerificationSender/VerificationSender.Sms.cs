using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Sending.VerificationSender;
public partial class VerificationSender
{
    private async Task<Result<bool>> SendCodePhone(
        VerificationCodePurpose purpose,
        string? phone,
        string code,
        string? pendingValue = null)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return Result<bool>.Fail(
                VerificationResources.NoUserPhone);
        }

        return purpose switch
        {
            VerificationCodePurpose.EnableTwoFactorPhone =>
                await _smsService.SendEnableTwoFactorCodeAsync(
                    phone, code),

            VerificationCodePurpose.DisableTwoFactorPhone =>
                await _smsService.SendDisableTwoFactorCodeAsync(
                    phone, code),

            VerificationCodePurpose.InitiateTwoFactorPhone =>
                await _smsService.SendInitiateTwoFactorCodeAsync(
                    phone, code),

            VerificationCodePurpose.ChangeNewPhone 
                when pendingValue is not null =>
                await _smsService.SendNewPhoneConfirmationCodeAsync(
                    pendingValue, code),

            VerificationCodePurpose.ChangeCurrentPhone
                when phone is not null =>
                await _smsService.SendPhoneChangeApprovalCodeAsync(
                    phone, code),

            VerificationCodePurpose.ResetPasswordPhone =>
                await _smsService.SendResetPasswordCodeAsync(
                    phone, code),

            VerificationCodePurpose.ChangePasswordPhone =>
                await _smsService.SendChangePasswordCodeAsync(
                    phone, code),

            VerificationCodePurpose.RegistrationPhone
                => await _smsService.SendVerificationCodeAsync(
                    phone, code),

            _ => Result<bool>.Fail(
                VerificationResources.UnsupportedVerificationPurpose)
        };
    }
}
