using ShagOxServer.Application.DTOs.Verifications.Create;
using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation.UserVerification;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Confirmation.UserVerification;
public partial class UserVerificationService
    : IUserVerificationService
{
    public async Task<Result<VerificationResponse>> ChangePasswordAsync(
        User user,
        VerificationCode verificationCode)
    {
        if (string.IsNullOrWhiteSpace(
                  verificationCode.PendingValue))
        {
            return Result<VerificationResponse>.Fail(
                VerificationResources.PendingPasswordNotFound);
        }

        user.PasswordHash =
            verificationCode.PendingValue
            ?? user.PasswordHash;

        await _trastManagement
            .UntrustDevicesAsync(user);

        await _trastManagement.RevokeSessionsByUserAsync(
            user,
            SessionRevocationReason.PasswordChanged);

        return Result<VerificationResponse>.Success(
            new VerificationResultResponse(true));
    }
}
