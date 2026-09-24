using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Common.Validators.Enum;
using ShagOxServer.Application.DTOs.Auth.Users.Contacts.Emails;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.Emails;
using ShagOxServer.Application.Interfaces.Services.Common.Validators;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.Auth.Contacts.Emails;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Contacts.Emails;
public class ChangeEmailService
    : IChangeEmailService
{
    private readonly UserValidator _userValidator;

    private readonly IContactValidator _contactValidator;

    private readonly IVerificationSender _verificationSender;
    private readonly ILogger<ChangeEmailService> _logger;


    public ChangeEmailService(
        UserValidator userValidator,
        IContactValidator contactValidator,
        IVerificationSender verificationSender,
        ILogger<ChangeEmailService> logger)
    {
        _userValidator = userValidator;
        _contactValidator = contactValidator;
        _verificationSender = verificationSender;
        _logger = logger;
    }

    public async Task<Result<bool>> ChangeEmail(
        int userId,
        ChangeEmailRequest request)
    {
        var user = await _userValidator
            .GetByIdAsync(userId);
        if (!user.IsSuccess)
            return Result<bool>.Fail(user.Error);

        var email = request.Email.Replace(" ", "");

        var type = _contactValidator.Detect(email);
        if (type != UserContactType.Email)
        {
            return Result<bool>
                .Fail(EmailAuth.InvalidEmail);
        }

        var emailExists = await _userValidator
            .NotExistsByEmailAsync(email);
        if (!emailExists.IsSuccess)
            return Result<bool>.Fail(emailExists.Error); 

        try
        {
            var result = await _verificationSender
                .SendAsync(user.Value!,
                    VerificationCodePurpose.ChangeEmail,
                    email
            );

            _logger.LogInformation(
                "Email changed successfully. UserId: {UserId}",
                user.Value!.Id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send verification code for email change. UserId: {UserId}",
                userId);

            return Result<bool>.Fail(
                EmailAuth.FailedToSendVerificationEmail);
        }
    }
}