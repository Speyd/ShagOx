using ShagOxServer.Application.Common.Validators;
using ShagOxServer.Application.DTOs.Auth.Users.Contacts.Emails;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts;
using ShagOxServer.Application.Interfaces.Services.Common.Validators;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Contacts;
public class ChangeEmailService
    : IChangeEmailService
{
    private readonly UserValidator _userValidator;

    private readonly IContactValidator _contactValidator;

    private readonly IVerificationSender _verificationSender;


    public ChangeEmailService(
        UserValidator userValidator,
        IContactValidator contactValidator,
        IVerificationSender verificationSender)
    {
        _userValidator = userValidator;
        _contactValidator = contactValidator;
        _verificationSender = verificationSender;
    }

    public async Task<Result<bool>> ChangeEmail(
        int userId,
        ChangeEmailRequest request)
    {
        var user = await _userValidator
            .GetByIdAsync(userId);
        if (!user.IsSuccess)
            return Result<bool>.Fail(user.Error);

        var type = _contactValidator.Detect(request.Email);
        if(type != UserContactType.Email)
            return Result<bool>.Fail("Email is incorrect");

        var emailExists = await _userValidator
            .NotExistsByEmailAsync(request.Email);
        if (!emailExists.IsSuccess)
            return Result<bool>.Fail(emailExists.Error);


        var result = await _verificationSender
            .SendAsync(user.Value!,
                VerificationCodePurpose.ChangeEmail,
                request.Email
            );

        return Result<bool>.Success(true);
    }
}