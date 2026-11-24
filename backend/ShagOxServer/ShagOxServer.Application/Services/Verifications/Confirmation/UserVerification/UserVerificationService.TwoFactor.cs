using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation.UserVerification;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Confirmation.UserVerification;
public partial class UserVerificationService
    : IUserVerificationService
{
    public async Task<Result<bool>> EnableTwoFactor(
        User user,
        VerificationCode verificationCode)
    {
        user.TwoFactorEnabled = true;

        await _trastManagement.RevokeSessionByUserAsync(
            user,
            SessionRevocationReason.TwoFactorEnabled);

        await _trastManagement
            .RevokeDeviceAsync(user);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> DisableTwoFactor(
        User user,
        VerificationCode verificationCode)
    {
        user.TwoFactorEnabled = false;

        await _trastManagement.RevokeSessionByUserAsync(
            user,
            SessionRevocationReason.TwoFactorEnabled);

        await _trastManagement
            .RevokeDeviceAsync(user);

        return Result<bool>.Success(true);
    }
}
