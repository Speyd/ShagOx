using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Resources.Auth.Registrations;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Account.Enum;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Register;

public partial class RegisterService
    : IRegisterService
{
    private async Task<Result<bool>> SendVerification(
        User user)
    {
        user.Status = UserStatus.PendingVerification;

        if (!string.IsNullOrWhiteSpace(user.Email))
        {
            return await _senderVerification
                .SendAsync(user,
                    VerificationCodePurpose.RegistrationEmail);
        }

        if (!string.IsNullOrWhiteSpace(user.Phone))
        {
            return await _senderVerification
                .SendAsync(user,
                    VerificationCodePurpose.RegistrationPhone);
        }

        return Result<bool>.Fail(
            RegistrationAuthResources.EmailOrPhoneRequired);
    }
}