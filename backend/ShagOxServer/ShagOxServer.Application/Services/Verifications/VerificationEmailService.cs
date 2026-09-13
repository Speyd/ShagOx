using ShagOxServer.Application.DTOs.Verifications.VerificationEmails;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Verifications;
using ShagOxServer.Domain.Entities.Account;
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

        var valid =
            await _verificationCodeService
                .VerifyCodeAsync(
                    user.Id,
                    request.Code);

        if (!valid)
            return Result<bool>.Fail(
                "Invalid or expired verification code.");

        user.EmailConfirmed = true;

        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }
}