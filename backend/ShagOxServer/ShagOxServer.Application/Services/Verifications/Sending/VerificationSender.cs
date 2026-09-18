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

                await SendCodeEmail(user, user.Email, code.Value!);

                user.EmailConfirmed = false;

                break;

            case VerificationCodePurpose.RegistrationPhone:

                await SendCodePhone(user, user.Phone, code.Value!);
   
                user.PhoneConfirmed = false;

                break;

            case VerificationCodePurpose.ChangeEmail:

                await SendCodeEmail(user, user.Email, code.Value!);

                break;

            case VerificationCodePurpose.ChangePhone:

                await SendCodePhone(user, user.Phone, code.Value!);

                break;

            case VerificationCodePurpose.ResetPasswordEmail:
            case VerificationCodePurpose.ChangePasswordEmail:

                await SendCodeEmail(user, user.Email, code.Value!);

                break;


            case VerificationCodePurpose.ResetPasswordPhone:
            case VerificationCodePurpose.ChangePasswordPhone:

                await SendCodePhone(user, user.Phone, code.Value!);

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

        await _emailService.SendVerificationCodeAsync(
            email,
            code);

        return Result<bool>.Success(true);

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

        await _smsService.SendVerificationCodeAsync(
            phone,
            code);

        return Result<bool>.Success(true);

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

        if (code is null)
        { 
            return Result<string>
                .Fail("Error during Code generation");
        }

        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Success(code);
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