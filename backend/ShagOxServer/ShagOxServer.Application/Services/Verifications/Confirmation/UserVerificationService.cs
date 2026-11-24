using ShagOxServer.Application.Interfaces.Services.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Domain.Entities.Auth.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Confirmation;
public class UserVerificationService
    : IUserVerificationService
{
    private readonly IUserTrastManagementService _trastManagement;


    public UserVerificationService(
        IUserTrastManagementService trastManagement)
    {
        _trastManagement = trastManagement;
    }

    public async Task<Result<bool>> ConfirmEmail(
       User user,
       VerificationCode verificationCode)
    {
        if (user.EmailConfirmed)
        {
            return Result<bool>.Fail(
                VerificationResources.EmailAlreadyConfirmed);
        }

        user.Email =
            verificationCode.PendingValue
            ?? user.Email;

        user.EmailConfirmed = true;
        user.Status = UserStatus.Active;

        await _trastManagement
            .TrustDeviceAsync(user);

        await _trastManagement.TrustSessionAsync(
            user, 
            SessionRevocationReason.ContactChanged);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ChangeEmail(
        User user,
        VerificationCode verificationCode)
    {
        if (string.IsNullOrWhiteSpace(
                   verificationCode.PendingValue))
        {
            return Result<bool>.Fail(
                VerificationResources.PendingEmailNotFound);
        }

        user.Email = verificationCode.PendingValue;
        user.EmailConfirmed = true;
        user.Status = UserStatus.Active;

        await _trastManagement
            .TrustDeviceAsync(user);

        await _trastManagement.TrustSessionAsync(
            user,
            SessionRevocationReason.ContactChanged);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ConfirmPhone(
        User user,
        VerificationCode verificationCode)
    {
        if (user.PhoneConfirmed)
        {
            return Result<bool>.Fail(
                VerificationResources.PhoneAlreadyConfirmed);
        }

        user.Phone =
            verificationCode.PendingValue
            ?? user.Phone;

        user.PhoneConfirmed = true;
        user.Status = UserStatus.Active;

        await _trastManagement
            .TrustDeviceAsync(user);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ChangePhone(
        User user,
        VerificationCode verificationCode)
    {

        if (string.IsNullOrWhiteSpace(
                  verificationCode.PendingValue))
        {
            return Result<bool>.Fail(
                VerificationResources.PendingPhoneNotFound);
        }

        user.Phone = verificationCode.PendingValue;
        user.PhoneConfirmed = true;
        user.Status = UserStatus.Active;

        await _trastManagement
            .TrustDeviceAsync(user);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ChangePassword(
        User user,
        VerificationCode verificationCode)
    {
        if (string.IsNullOrWhiteSpace(
                  verificationCode.PendingValue))
        {
            return Result<bool>.Fail(
                VerificationResources.PendingPasswordNotFound);
        }

        user.PasswordHash =
            verificationCode.PendingValue
            ?? user.PasswordHash;

        await _trastManagement
            .TrustDeviceAsync(user);

        await _trastManagement.TrustSessionAsync(
            user,
            SessionRevocationReason.PasswordChanged);

        return Result<bool>.Success(true);
    }
}
