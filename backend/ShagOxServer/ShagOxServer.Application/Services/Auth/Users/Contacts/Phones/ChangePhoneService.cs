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


    public ChangePhoneService(
        UserValidator userValidator,
        IContactValidator contactValidator,
        IVerificationSender verificationSender)
    {
        _userValidator = userValidator;
        _contactValidator = contactValidator;
        _verificationSender = verificationSender;
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


        var result = await _verificationSender
            .SendAsync(user.Value!,
                VerificationCodePurpose.ChangePhone,
                phone
            );

        return Result<bool>.Success(true);
    }
}