using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.TwoFactor;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.Auth.Contacts.Emails;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Contacts.TwoFactor;
public class TwoFactorService
    : ITwoFactorService
{
    private readonly UserValidator _userValidator;
    private readonly IUserContext _userContext;

    private readonly IVerificationSender _verificationSender;
    private readonly ILogger<TwoFactorService> _logger;


    public TwoFactorService(
        UserValidator userValidator,
        IUserContext userContext,
        IVerificationSender verificationSender,
        ILogger<TwoFactorService> logger)
    {
        _userValidator = userValidator;
        _userContext = userContext;
        _verificationSender = verificationSender;
        _logger = logger;
    }

    public async Task<Result<bool>> Enable()
    {
        var userId = _userContext.UserId;
        if (!userId.HasValue)
        {
            return Result<bool>.NotFound(
                EntityNamesResources.User);
        }

        var user = await _userValidator
            .GetByIdAsync(userId.Value);
        if (!user.IsSuccess)
            return Result<bool>.Fail(user.Error);

        if(user.Value?.TwoFactorEnabled == true)
            return Result<bool>.Success(true);

        try
        {
            var result = await _verificationSender
                .SendAsync(user.Value!,
                    VerificationCodePurpose.EnableTwoFactor);

            _logger.LogInformation(
                "Enable Two factor successfully. UserId: {UserId}",
                user.Value!.Id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send verification code for two factor enable. UserId: {UserId}",
                userId);

            return Result<bool>.Fail(
                EmailAuthResources.FailedToSendVerificationTwoFactor);
        }
    }
}
