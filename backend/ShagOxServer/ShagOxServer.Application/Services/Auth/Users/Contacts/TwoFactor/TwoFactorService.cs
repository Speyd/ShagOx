using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.TwoFactor;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.Auth.Contacts.Emails;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Contacts.TwoFactor;
public class TwoFactorService
    : ITwoFactorService
{
    private readonly UserValidator _userValidator;
    private readonly IUserContext _userContext;

    private readonly IUserDeviceQueryService _deviceService;

    private readonly IVerificationSender _verificationSender;
    private readonly ILogger<TwoFactorService> _logger;


    public TwoFactorService(
        UserValidator userValidator,
        IUserContext userContext,
        IUserDeviceQueryService deviceService,
        IVerificationSender verificationSender,
        ILogger<TwoFactorService> logger)
    {
        _userValidator = userValidator;
        _userContext = userContext;
        _deviceService = deviceService;
        _verificationSender = verificationSender;
        _logger = logger;
    }


    public async Task<Result<bool>> EnableAsync(
        CancellationToken cancellationToken)
    {
        return await ChangeTwoFactorStatusAsync(
            VerificationCodePurpose.EnableTwoFactorEmail,
            VerificationCodePurpose.EnableTwoFactorPhone,
            true,
            cancellationToken);
    }

    public async Task<Result<bool>> DisableAsync(
        CancellationToken cancellationToken)
    {
        return await ChangeTwoFactorStatusAsync(
            VerificationCodePurpose.DisableTwoFactorEmail,
            VerificationCodePurpose.DisableTwoFactorPhone,
            false,
            cancellationToken);
    }

    public async Task<Result<LoginResponse>> InitiateAsync(
        User user,
        long deviceId)
    {
        var device = await _deviceService
            .GetByIdAsync(deviceId);

        if(device is null)
        {
            return Result<LoginResponse>
                .Fail(EntityNamesResources.UserDevice);
        }

        var purpose = user.EmailConfirmed
            ? VerificationCodePurpose.InitiateTwoFactorEmail
            : user.PhoneConfirmed
                ? VerificationCodePurpose.InitiateTwoFactorPhone
                : VerificationCodePurpose.None;

        var result = await _verificationSender.SendAsync(
            user,
            purpose,
            deviceId.ToString());

        if (!result.IsSuccess)
        {
            return Result<LoginResponse>
                .Fail(result.Error);
        }

        return Result<LoginResponse>.Success(
            new LoginResponse(
                null,
                null));
    }

    private async Task<Result<bool>> ChangeTwoFactorStatusAsync(
        VerificationCodePurpose purposeEmail,
        VerificationCodePurpose purposePhone,
        bool enable,
        CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;

        if (!userId.HasValue)
        {
            return Result<bool>.NotFound(
                EntityNamesResources.User);
        }

        var userResult = await _userValidator.GetByIdAsync(
            userId.Value,
            cancellationToken);

        if (!userResult.IsSuccess || userResult.Value is null)
            return Result<bool>.Fail(userResult.Error);

        var user = userResult.Value;

        if (user.TwoFactorEnabled == enable)
            return Result<bool>.Success(true);

        try
        {
            var purpose = user.EmailConfirmed
                ? purposeEmail
                : user.PhoneConfirmed
                    ? purposePhone
                    : VerificationCodePurpose.None;

            var result = await _verificationSender.SendAsync(
                user,
                purpose);

            if (!result.IsSuccess)
                return result;

            _logger.LogInformation(
                "Two-factor {Action} verification sent. UserId: {UserId}",
                enable ? "enable" : "disable",
                user.Id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send two-factor {Action} verification. UserId: {UserId}",
                enable ? "enable" : "disable",
                user.Id);

            return Result<bool>.Fail(
                EmailAuthResources.FailedToSendVerificationTwoFactor);
        }
    }
}
