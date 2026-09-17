using Microsoft.AspNetCore.Identity;
using ShagOxServer.Application.DTOs.Auth.Users.Contacts.Passwords;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.Passwords;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Contacts.Passwords;

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
        if (string.IsNullOrWhiteSpace(request.NewPassword))
            return Result<bool>.Fail("New Password is incorrect!");

        var user = await _userValidator
            .GetByIdAsync(userId);
        if (!user.IsSuccess)
            return Result<bool>.Fail(user.Error);

        var verifyResult = _passwordHasher.VerifyHashedPassword(
            user.Value!,
            user.Value!.PasswordHash,
            request.OldPassword);

        if (verifyResult == PasswordVerificationResult.Failed)
            return Result<bool>.Fail("Invalid password");

        string newPasswordHash =
           _passwordHasher.HashPassword(
               user.Value!,
               request.NewPassword
           );

        var result = await SendPasswordChangeCode(
            user.Value!,
            newPasswordHash);

        if (!result.IsSuccess)
            return Result<bool>.Fail(result.Error);

        return Result<bool>.Success(true);
    }

    private async Task<Result<bool>> SendPasswordChangeCode(
        User user,
        string newPasswordHash)
    {
        if (user.EmailConfirmed)
        {
            return await SendChangeCode(
                user,
                VerificationCodePurpose.ChangePasswordEmail,
                newPasswordHash);
        }

        if (user.PhoneConfirmed)
        {
            return await SendChangeCode(
                user,
                VerificationCodePurpose.ChangePasswordPhone,
                newPasswordHash);
        }

        return Result<bool>.Fail(
            "No confirmed email or phone found.");
    }

    private async Task<Result<bool>> SendChangeCode(
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