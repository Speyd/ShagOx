using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Account.Enum;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Confirmation;
public class UserVerificationService
    : IUserVerificationService
{
    public Result<bool> ConfirmEmail(
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

        return Result<bool>.Success(true);
    }

    public Result<bool> ChangeEmail(
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

        return Result<bool>.Success(true);
    }

    public Result<bool> ConfirmPhone(
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

        return Result<bool>.Success(true);
    }

    public Result<bool> ChangePhone(
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

        return Result<bool>.Success(true);
    }

    public Result<bool> ChangePassword(
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

        return Result<bool>.Success(true);
    }
}