using Microsoft.AspNetCore.Identity;
using ShagOxServer.Application.DTOs.Auth.Users.Contacts.Passwords;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Contacts;

public class ChangePasswordService
    : IChangePasswordService
{
    private readonly UserValidator _userValidator;

    private readonly IVerificationSender _verificationSender;
    private readonly IPasswordHasher<User> _passwordHasher;


    public ChangePasswordService(
        UserValidator userValidator,
        IVerificationSender verificationSender,
        IPasswordHasher<User> passwordHasher)
    {
        _userValidator = userValidator;
        _verificationSender = verificationSender;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<bool>> ChangePassword(
        int userId,
        ChangePasswordRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.newPassword))
            return Result<bool>.Fail("New Password is incorrect!");

        var user = await _userValidator
            .GetByIdAsync(userId);
        if (!user.IsSuccess)
            return Result<bool>.Fail(user.Error);

        var verifyResult = _passwordHasher.VerifyHashedPassword(
            user.Value!,
            user.Value!.PasswordHash,
            request.oldPassword);

        if (verifyResult == PasswordVerificationResult.Failed)
            return Result<bool>.Fail("Invalid password");

        string newPasswordHash =
           _passwordHasher.HashPassword(
               user.Value!,
               request.newPassword
           );

        var result = await SendPasswordResetCode(
            user.Value!,
            newPasswordHash);

        if (!result.IsSuccess)
            return Result<bool>.Fail(result.Error);

        return Result<bool>.Success(true);
    }

    private async Task<Result<bool>> SendPasswordResetCode(
        User user,
        string newPasswordHash)
    {
        if (user.EmailConfirmed)
        {
            return await SendResetCode(
                user,
                VerificationCodePurpose.ResetPasswordEmail,
                newPasswordHash);
        }

        if (user.PhoneConfirmed)
        {
            return await SendResetCode(
                user,
                VerificationCodePurpose.ResetPasswordPhone,
                newPasswordHash);
        }

        return Result<bool>.Fail(
            "No confirmed email or phone found.");
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

        if (!result.IsSuccess)
            return Result<bool>.Fail(result.Error);

        return Result<bool>.Success(true);
    }
}