using ShagOxServer.Application.Common.Validators.Enum;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Services.Common.Validators;
using ShagOxServer.Application.Resources.Auth.Registrations;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Contacts;
public class UserContactApplier
{
    private readonly UserValidator _userValidator;
    private readonly IContactValidator _contactValidator;

    public UserContactApplier(
        UserValidator userValidator,
        IContactValidator contactValidator)
    {
        _userValidator = userValidator;
        _contactValidator = contactValidator;
    }

    public async Task<Result<UserContactType>> ApplyAsync(
        User user,
        RegisterRequest request)
    {
        var EmailOrPhone = request.EmailOrPhone.Replace(" ", "");
        var UserName = request.UserName.Replace(" ", "");

        var type =
            _contactValidator.Detect(EmailOrPhone);  

        var contactResult = type switch
        {
            UserContactType.Email =>
                await ApplyEmailAsync(user, EmailOrPhone),

            UserContactType.Phone =>
                await ApplyPhoneAsync(user, EmailOrPhone),

            _ => Result<bool>.Fail(
                RegistrationAuthResources.UnsupportedContactType)
        };

        if (!contactResult.IsSuccess)
            return Result<UserContactType>.Fail(contactResult.Error);

        var userNameResult =
            await ApplyUserNameAsync(user, UserName);

        if (!userNameResult.IsSuccess)
            return Result<UserContactType>.Fail(userNameResult.Error);

        return Result<UserContactType>.Success(type);
    }

    private async Task<Result<bool>> ApplyEmailAsync(
        User user,
        string email)
    {
        if (user.Email != email)
        {
            var result =
                await _userValidator.NotExistsByEmailAsync(email);

            if (!result.IsSuccess)
                return Result<bool>.Fail(result.Error);
        }

        user.Email = email;

        return Result<bool>.Success(true);
    }

    private async Task<Result<bool>> ApplyPhoneAsync(
        User user,
        string phone)
    {
        if (user.Phone != phone)
        {
            var result =
                await _userValidator.NotExistsByPhoneAsync(phone);
          
            if (!result.IsSuccess)
                return Result<bool>.Fail(result.Error);
        }

        user.Phone = phone;

        return Result<bool>.Success(true);
    }

    private async Task<Result<bool>> ApplyUserNameAsync(
        User user,
        string userName)
    {
        if (user.UserName != userName)
        {
            var result = await _userValidator
                .NotExistsByUserNameAsync(userName);

            if (!result.IsSuccess)
                return Result<bool>.Fail(result.Error);
        }

        user.UserName = userName;

        return Result<bool>.Success(true);
    }
}