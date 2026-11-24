using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Services.Verifications.Codes;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;


namespace ShagOxServer.Application.Services.Verifications.Sending.VerificationSender;
public partial class VerificationSender : IVerificationSender
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

        Result<bool> sendResult = purpose switch
        {
            VerificationCodePurpose.RegistrationEmail
                or VerificationCodePurpose.ChangeEmail
                or VerificationCodePurpose.ResetPasswordEmail
                or VerificationCodePurpose.ChangePasswordEmail
                or VerificationCodePurpose.EnableTwoFactorEmail
                or VerificationCodePurpose.DisableTwoFactorEmail
                or VerificationCodePurpose.InitiateTwoFactorEmail
                => await SendCodeEmail(
                    purpose, user.Email, code.Value!),

            VerificationCodePurpose.RegistrationPhone
                or VerificationCodePurpose.ChangePhone
                or VerificationCodePurpose.ResetPasswordPhone
                or VerificationCodePurpose.ChangePasswordPhone
                or VerificationCodePurpose.EnableTwoFactorPhone
                or VerificationCodePurpose.DisableTwoFactorPhone
                or VerificationCodePurpose.InitiateTwoFactorPhone

                => await SendCodePhone(
                    purpose, user.Phone, code.Value!),

            _ => Result<bool>.Fail(
                VerificationResources.UnsupportedVerificationPurpose)
        };

        if (!sendResult.IsSuccess)
            return Result<bool>.Fail(sendResult.Error);

        ApplyConfirmationStatus(user, purpose);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> SendAsync(
        long userId,
        VerificationCodePurpose purpose,
        string? pendingValue = null)
    {
        var user = await _userValidator.GetByIdAsync(userId);

        if (!user.IsSuccess)
            return Result<bool>.Fail(user.Error);

        return await SendAsync(user.Value!, purpose, pendingValue);
    }
}
