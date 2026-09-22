using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation;
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
            return Result<bool>
                .Fail("Email already confirmed.");
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
                "Pending email not found.");
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
            return Result<bool>
                .Fail("Phone already confirmed.");
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
                "Pending phone not found.");
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
                "Pending password not found.");
        }

        user.PasswordHash =
            verificationCode.PendingValue
            ?? user.PasswordHash;

        return Result<bool>.Success(true);
    }
}