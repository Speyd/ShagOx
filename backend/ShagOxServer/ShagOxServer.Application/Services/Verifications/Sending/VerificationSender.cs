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

    private readonly IUnitOfWork _unitOfWork;


    public VerificationSender(
        UserValidator userValidator,
        IVerificationCodeService codeService,
        IEmailService emailService,
        IUnitOfWork unitOfWork)
    {
        _userValidator = userValidator;
        _codeService = codeService;
        _emailService = emailService;
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
            case VerificationCodePurpose.ChangeEmail:
            case VerificationCodePurpose.ResetPassword:

                string? email = pendingValue ?? user.Email; 

                if (string.IsNullOrWhiteSpace(email))
                    return Result<bool>.Fail(
                        "User does not have an email.");

                user.EmailConfirmed = false;

                await _emailService
                    .SendVerificationCodeAsync(
                        email,
                        code.Value!);

                break;

            case VerificationCodePurpose.RegistrationPhone:
            case VerificationCodePurpose.ChangePhone:

                if (string.IsNullOrWhiteSpace(user.Phone))
                    return Result<bool>.Fail(
                        "User does not have a phone.");

                user.PhoneConfirmed = false;

                break;

            default:
                return Result<bool>.Fail(
                    "Unsupported verification purpose.");
        }

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
        user.Status = UserStatus.PendingVerification;

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
}