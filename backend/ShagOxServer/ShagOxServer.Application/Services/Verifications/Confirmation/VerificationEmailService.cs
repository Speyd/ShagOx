using ShagOxServer.Application.DTOs.Verifications.VerificationEmails;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Verifications.Codes;
using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Account.Enum;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;
namespace ShagOxServer.Application.Services.Verifications.Confirmation;
public class VerificationEmailService
    : Verification,
    IVerificationEmailService
{
    private readonly IRepository<User> _userRepository;

    private readonly IVerificationCodeService
        _verificationCodeService;

    private readonly IUnitOfWork _unitOfWork;

    public VerificationEmailService(
        IRepository<User> userRepository,
        IVerificationCodeService verificationCodeService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _verificationCodeService = verificationCodeService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> VerifyAsync(
        VerificationEmailRequest request)
    {
        var user = await _userRepository
            .GetByIdAsync(request.UserId!.Value);

        if (user is null)
            return Result<bool>.NotFound(typeof(User));

        var resultVerify = await _verificationCodeService
            .VerifyCodeAsync(user.Id, request.Code);

        var mapResult = MapVerificationResult(resultVerify.Result);
        if(!mapResult.IsSuccess)
            return Result<bool>.Fail(mapResult.Error);

        var resultApply = ApplyEmailVerification(user, resultVerify.Code);
        if (!resultApply.IsSuccess)
            return Result<bool>.Fail(resultApply.Error);

        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }

    private Result<bool> ApplyEmailVerification(
        User user,
        VerificationCode? verificationCode)
    {
        switch (verificationCode?.Purpose)
        {
            case VerificationCodePurpose.RegistrationEmail:

                if (user.EmailConfirmed)
                    return Result<bool>.Fail("Email already confirmed.");

                user.Email =
                    verificationCode.PendingValue
                    ?? user.Email;

                break;

            case VerificationCodePurpose.ChangeEmail:

                if (string.IsNullOrWhiteSpace(
                    verificationCode.PendingValue))
                {
                    return Result<bool>.Fail(
                        "Pending email not found.");
                }

                user.Email = verificationCode.PendingValue;

                break;

            default:
                return Result<bool>.Fail(
                    "Invalid verification purpose.");
        }

        user.EmailConfirmed = true;
        user.Status = UserStatus.Active;

        return Result<bool>.Success(true);
    }
}