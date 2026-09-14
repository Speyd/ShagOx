using ShagOxServer.Application.DTOs.Verifications.VerificationEmails;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Verifications;
using ShagOxServer.Application.Services.Verifications.Enum;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Account.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;
namespace ShagOxServer.Application.Services.Verifications;
public class VerificationEmailService
    : IVerificationEmailService
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
            .GetByIdAsync(request.UserId);
        


        if (user is null)
            return Result<bool>.Fail("User not found.");

        if (user.EmailConfirmed)
            return Result<bool>.Fail("Email already confirmed.");

        var result = await _verificationCodeService
            .VerifyCodeAsync(user.Id, request.Code);

        switch (result)
        {
            case VerificationCodeResult.NotFound:
                return Result<bool>.Fail(
                    "Verification code not found.");

            case VerificationCodeResult.Expired:
                return Result<bool>.Fail(
                    "Verification code has expired.");

            case VerificationCodeResult.Invalid:
                return Result<bool>.Fail(
                    "Invalid verification code.");

            case VerificationCodeResult.AttemptsExceeded:
                return Result<bool>.Fail(
                    "Too many attempts.");

            case VerificationCodeResult.AlreadyUsed:
                return Result<bool>.Fail(
                    "Verification code has already been used.");

            case VerificationCodeResult.Success:
                break;

            default:
                return Result<bool>.Fail(
                    "Unknown verification result.");
        }

        user.EmailConfirmed = true;
        user.Status = UserStatus.Active;

        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }
}