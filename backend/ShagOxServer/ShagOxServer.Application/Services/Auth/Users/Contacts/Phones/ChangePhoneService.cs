using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Common.Validators.Enum;
using ShagOxServer.Application.DTOs.Auth.Users.Contacts.Phones;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.Phones;
using ShagOxServer.Application.Interfaces.Services.Common.Validators;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Contacts.Phones;
public class ChangePhoneService
    : IChangePhoneService
{
    private readonly UserValidator _userValidator;

    private readonly IContactValidator _contactValidator;

    private readonly IVerificationSender _verificationSender;
    private readonly ILogger<ChangePhoneService> _logger;


    public ChangePhoneService(
        UserValidator userValidator,
        IContactValidator contactValidator,
        IVerificationSender verificationSender,
        ILogger<ChangePhoneService> logger)
    {
        _userValidator = userValidator;
        _contactValidator = contactValidator;
        _verificationSender = verificationSender;
        _logger = logger;
    }

    public async Task<Result<bool>> ChangePhone(
        int userId,
        ChangePhoneRequest request)
    {
        var user = await _userValidator
            .GetByIdAsync(userId);
        if (!user.IsSuccess)
            return Result<bool>.Fail(user.Error);

        var phone = request.Phone.Replace(" ", "");

        var type = _contactValidator.Detect(phone);
        if (type != UserContactType.Phone)
            return Result<bool>.Fail("Phone is incorrect");

        var phoneExists = await _userValidator
            .NotExistsByPhoneAsync(phone);
        if (!phoneExists.IsSuccess)
            return Result<bool>.Fail(phoneExists.Error);

        try
        {
            var result = await _verificationSender
                .SendAsync(user.Value!,
                    VerificationCodePurpose.ChangePhone,
                    phone
                );

            _logger.LogInformation(
                "Phone changed successfully. UserId: {UserId}",
                user.Value!.Id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send verification code for phone change. UserId: {UserId}",
                userId);

            return Result<bool>.Fail(
                "Failed to send verification code.");
        }
    }
}