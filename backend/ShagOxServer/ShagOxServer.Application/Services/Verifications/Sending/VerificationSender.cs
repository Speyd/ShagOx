using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Services.Verifications.Codes;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Account.Enum;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Sending;
public class VerificationSender
    : IVerificationSender
{
    private readonly UserValidator _userValidator;

    private readonly IVerificationCodeService _codeService;

    private readonly IEmailService _emailService;

    private readonly ISmsService _smsService;

    private readonly IUnitOfWork _unitOfWork;


    public VerificationSender(
        UserValidator userValidator,
        IVerificationCodeService codeService,
        IEmailService emailService,
        ISmsService smsService,
        IUnitOfWork unitOfWork)
    {
        _userValidator = userValidator;
        _codeService = codeService;
        _emailService = emailService;
        _smsService = smsService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> SendAsync(
        User user,
        VerificationCodePurpose purpose,
        string? pendingValue = null)
    {
        var code = await SendCode(user, purpose, pendingValue);
        if (!code.IsSuccess)
            return Result<bool>.Fail(code.Error);
        
        switch (purpose)
        {
            case VerificationCodePurpose.RegistrationEmail:

                var regEmail = await SendCodeEmail(user, user.Email, code.Value!);
                if (!regEmail.IsSuccess)
                    return Result<bool>.Fail(regEmail.Error);

                user.EmailConfirmed = false;

                break;

            case VerificationCodePurpose.RegistrationPhone:

                var regPhone = await SendCodePhone(user, user.Phone, code.Value!);
                if (!regPhone.IsSuccess)
                    return Result<bool>.Fail(regPhone.Error);

                user.PhoneConfirmed = false;

                break;

            case VerificationCodePurpose.ChangeEmail:

                var changeEmail = await SendCodeEmail(user, user.Email, code.Value!);
                if (!changeEmail.IsSuccess)
                    return Result<bool>.Fail(changeEmail.Error);

                break;

            case VerificationCodePurpose.ChangePhone:

                var changePhone = await SendCodePhone(user, user.Phone, code.Value!);
                if (!changePhone.IsSuccess)
                    return Result<bool>.Fail(changePhone.Error);

                break;

            case VerificationCodePurpose.ResetPasswordEmail:
            case VerificationCodePurpose.ChangePasswordEmail:

                var passwordEmail = await SendCodeEmail(user, user.Email, code.Value!);
                if (!passwordEmail.IsSuccess)
                    return Result<bool>.Fail(passwordEmail.Error);

                break;


            case VerificationCodePurpose.ResetPasswordPhone:
            case VerificationCodePurpose.ChangePasswordPhone:

                var passwordPhone = await SendCodePhone(user, user.Phone, code.Value!);
                if (!passwordPhone.IsSuccess)
                    return Result<bool>.Fail(passwordPhone.Error);

                break;

            default:
                return Result<bool>.Fail(
                    "Unsupported verification purpose.");
        }

        return Result<bool>.Success(true);
    }

    private async Task<Result<bool>> SendCodeEmail(
        User user,
        string? email,
        string code)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result<bool>.Fail(
                "User does not have an email.");
        }

        return await _emailService.SendVerificationCodeAsync(
            email,
            code);
    }

    private async Task<Result<bool>> SendCodePhone(
        User user,
        string? phone,
        string code)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return Result<bool>.Fail(
                "User does not have an phone.");
        }

        return await _smsService.SendVerificationCodeAsync(
            phone,
            code);

    }

    public async Task<Result<bool>> SendAsync(
        int userId,
        VerificationCodePurpose purpose,
        string? pendingValue = null)
    {
        var user = await _userValidator
            .GetByIdAsync(userId);
        if (!user.IsSuccess)
            return Result<bool>.Fail(user.Error);

        return await SendAsync(user.Value!, purpose, pendingValue);
    }

    public async Task<Result<string>> SendCode(
        User user,
        VerificationCodePurpose purpose,
        string? pendingValue)
    {
        ApplyPendingStatus(user, purpose);

        var code = await _codeService
                .CreateCodeAsync(user.Id, purpose, pendingValue);

        if (!code.IsSuccess || 
            code.Value is null)
        { 
            return Result<string>
                .Fail("Error during Code generation");
        }

        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Success(code.Value!);
    }

    private static void ApplyPendingStatus(
        User user,
        VerificationCodePurpose purpose)
    {
        if (purpose is
            VerificationCodePurpose.RegistrationEmail or
            VerificationCodePurpose.RegistrationPhone)
        {
            user.Status = UserStatus.PendingVerification;
        }
    }
}