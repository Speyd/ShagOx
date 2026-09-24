using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Common.Validators.Enum;
using ShagOxServer.Application.DTOs.Auth.Users.Contacts.Passwords;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.Passwords;
using ShagOxServer.Application.Interfaces.Services.Common.Validators;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.Auth.Contacts.Passwords;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Contacts.Passwords;
public class ResetPasswordService
    : IResetPasswordService
{
    private readonly UserValidator _userValidator;

    private readonly IContactValidator _contactValidator;

    private readonly IVerificationSender _verificationSender;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ILogger<ResetPasswordService> _logger;


    public ResetPasswordService(
        UserValidator userValidator,
        IContactValidator contactValidator,
        IVerificationSender verificationSender,
        IPasswordHasher<User> passwordHasher,
        ILogger<ResetPasswordService> logger)
    {
        _userValidator = userValidator;
        _contactValidator = contactValidator;
        _verificationSender = verificationSender;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<Result<bool>> ResetPassword(
        ResetPasswordRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.NewPassword))
        {
            return Result<bool>
                .Fail(PasswordAuth.InvalidNewPassword);
        }

        var user = await _userValidator
            .GetByIdAsync(request.UserId);
        if (!user.IsSuccess)
            return Result<bool>.Fail(user.Error);

        string newPasswordHash =
           _passwordHasher.HashPassword(
               user.Value!,
               request.NewPassword
           );

        var type = _contactValidator.Detect(
            request.EmailOrPhoneOrUserName);

        try
        {
            var result = await SendPasswordResetCode(
                user.Value!,
                type,
                newPasswordHash);

            _logger.LogInformation(
                "Password reseted successfully. UserId: {UserId}",
                user.Value!.Id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to reset pasword. UserId: {UserId}",
                request.UserId);

            return Result<bool>
                .Fail(PasswordAuth.FailedToResetPassword);
        }
    }

    private async Task<Result<bool>> SendPasswordResetCode(
    User user,
    UserContactType type,
    string newPasswordHash)
    {
        VerificationCodePurpose purpose;

        switch (type)
        {
            case UserContactType.Email:
                if (!user.EmailConfirmed)
                {
                    return Result<bool>
                        .Fail(PasswordAuth.UserDoesNotHaveConfirmedEmail);
                }

                purpose = VerificationCodePurpose.ResetPasswordEmail;
                break;

            case UserContactType.Phone:
                if (!user.PhoneConfirmed)
                {
                    return Result<bool>
                        .Fail(PasswordAuth.UserDoesNotHaveConfirmedPhone);
                }

                purpose = VerificationCodePurpose.ResetPasswordPhone;
                break;

            case UserContactType.UserName:
                if (user.EmailConfirmed)
                {
                    purpose = VerificationCodePurpose.ResetPasswordEmail;
                    break;
                }

                if (user.PhoneConfirmed)
                {
                    purpose = VerificationCodePurpose.ResetPasswordPhone;
                    break;
                }

                return Result<bool>
                    .Fail(PasswordAuth.NoConfirmedContact);

            default:
                return Result<bool>
                    .Fail(PasswordAuth.InvalidContactType);
        }

        return await SendResetCode(
            user,
            purpose,
            newPasswordHash);
    }


    private async Task<Result<bool>> SendResetCode(
        User user,
        VerificationCodePurpose purpose,
        string newPasswordHash)
    {
        var result = await _verificationSender.SendAsync(
            user,
            purpose,
            newPasswordHash);

        return result;
    }
}