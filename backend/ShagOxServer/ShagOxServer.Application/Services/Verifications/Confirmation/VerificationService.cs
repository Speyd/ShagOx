using ShagOxServer.Application.DTOs.Verifications;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Verifications.Codes;
using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Application.Services.Verifications.Mappers;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Confirmation;
public class VerificationService
    : IVerificationService
{
    private readonly IRepository<User> _userRepository;
    private readonly IUserVerificationService _userVerifyService;

    private readonly IVerificationCodeService
        _verificationCodeService;

    private readonly IUnitOfWork _unitOfWork;

    public VerificationService(
        IRepository<User> userRepository,
        IUserVerificationService userVerifyService,
        IVerificationCodeService verificationCodeService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userVerifyService = userVerifyService;
        _verificationCodeService = verificationCodeService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> VerifyAsync(
        VerificationRequest request)
    {
        var user = await _userRepository
            .GetByIdAsync(request.UserId!.Value);

        if (user is null)
            return Result<bool>.NotFound(typeof(User));

        var resultVerify = await _verificationCodeService
            .VerifyCodeAsync(user.Id, request.Code);

        var mapResult = VerificationResultMapper
            .MapVerificationResult(resultVerify.Result);

        if(!mapResult.IsSuccess)
            return Result<bool>.Fail(mapResult.Error);

        var resultApply = ApplyVerification(user, resultVerify.Code);
        if (!resultApply.IsSuccess)
            return Result<bool>.Fail(resultApply.Error);

        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }

    private Result<bool> ApplyVerification(
        User user,
        VerificationCode? verificationCode)
    {
        switch (verificationCode?.Purpose)
        {
            case VerificationCodePurpose.RegistrationEmail:

                return _userVerifyService
                    .ConfirmEmail(user, verificationCode);

            case VerificationCodePurpose.RegistrationPhone:

                return _userVerifyService
                    .ConfirmPhone(user, verificationCode);

            case VerificationCodePurpose.ChangeEmail:

                return _userVerifyService
                     .ChangeEmail(user, verificationCode);

            case VerificationCodePurpose.ChangePhone:

                return _userVerifyService
                     .ChangePhone(user, verificationCode);

            case VerificationCodePurpose.ChangePasswordEmail:
            case VerificationCodePurpose.ChangePasswordPhone:
            case VerificationCodePurpose.ResetPasswordEmail:
            case VerificationCodePurpose.ResetPasswordPhone:

                return _userVerifyService
                    .ChangePassword(user, verificationCode);

            default:
                return Result<bool>.Fail(
                    VerificationResources.UnsupportedVerificationPurpose);
        }
    }
}