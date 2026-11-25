using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Sending.VerificationSender;
public partial class VerificationSender
{
    private async Task<Result<bool>> SendCodeEmail(
        VerificationCodePurpose purpose,
        string? email,
        string code)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result<bool>.Fail(
                VerificationResources.NoUserEmail);
        }

        return purpose switch
        {
            VerificationCodePurpose.EnableTwoFactorEmail =>
                await _emailService.SendEnableTwoFactorCodeAsync(
                    email, code),

            VerificationCodePurpose.DisableTwoFactorEmail =>
                await _emailService.SendDisableTwoFactorCodeAsync(
                    email, code),

            VerificationCodePurpose.InitiateTwoFactorEmail =>
                await _emailService.SendInitiateTwoFactorCodeAsync(
                    email, code),

            _ => await _emailService.SendVerificationCodeAsync(
                email, code)
        };
    }
}
