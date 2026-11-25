using ShagOxServer.Application.DTOs.Verifications.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Verifications.Codes;
using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation;
using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation.UserVerification;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Application.Services.Verifications.Mappers;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Confirmation;
public class VerificationService
    : IVerificationService
{
    private readonly IRepository<User> _userRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUserVerificationService _userVerifyService;

    private readonly IVerificationCodeService
        _verificationCodeService;

    private readonly IUnitOfWork _unitOfWork;

    public VerificationService(
        IRepository<User> userRepository,
        IUserQueryRepository userQueryRepository,
        IUserVerificationService userVerifyService,
        IVerificationCodeService verificationCodeService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userQueryRepository = userQueryRepository;
        _userVerifyService = userVerifyService;
        _verificationCodeService = verificationCodeService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<VerificationResponse>> VerifyAsync(
        VerificationRequest request)
    {
        var user = await _userQueryRepository
            .GetByIdAsync(request.UserId!.Value);

        if (user is null)
        {
            return Result<VerificationResponse>
                .NotFound(EntityNamesResources.User);
        }

        var resultVerify = await _verificationCodeService
            .VerifyCodeAsync(user.Id, request.Code);

        var mapResult = VerificationResultMapper
            .MapVerificationResult(resultVerify.Result);

        if (!mapResult.IsSuccess)
        {
            return Result<VerificationResponse>
                .Fail(mapResult.Error);
        }


        var resultApply = await ApplyVerification(user, resultVerify.Code);
        if (!resultApply.IsSuccess)
        {
            return Result<VerificationResponse>
                .Fail(resultApply.Error);
        }

        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync();

        return resultApply;
    }

    private async Task<Result<VerificationResponse>> ApplyVerification(
        User user,
        VerificationCode? verificationCode)
    {
        switch (verificationCode?.Purpose)
        {
            case VerificationCodePurpose.RegistrationEmail:

                return await _userVerifyService
                    .ConfirmEmailAsync(user, verificationCode);

            case VerificationCodePurpose.RegistrationPhone:

                return await _userVerifyService
                    .ConfirmPhoneAsync(user, verificationCode);

            case VerificationCodePurpose.ChangeEmail:

                return await _userVerifyService
                     .ChangeEmailAsync(user, verificationCode);

            case VerificationCodePurpose.ChangePhone:

                return await _userVerifyService
                     .ChangePhoneAsync(user, verificationCode);

            case VerificationCodePurpose.ChangePasswordEmail:
            case VerificationCodePurpose.ChangePasswordPhone:
            case VerificationCodePurpose.ResetPasswordEmail:
            case VerificationCodePurpose.ResetPasswordPhone:

                return await _userVerifyService
                    .ChangePasswordAsync(user, verificationCode);


            case VerificationCodePurpose.EnableTwoFactorEmail:
            case VerificationCodePurpose.EnableTwoFactorPhone:

                return await _userVerifyService
                     .EnableTwoFactorAsync(user, verificationCode);

            case VerificationCodePurpose.DisableTwoFactorEmail:
            case VerificationCodePurpose.DisableTwoFactorPhone:

                return await _userVerifyService
                     .DisableTwoFactorAsync(user, verificationCode);

            case VerificationCodePurpose.InitiateTwoFactorEmail:
            case VerificationCodePurpose.InitiateTwoFactorPhone:

                return await _userVerifyService
                     .InitiateTwoFactorAsync(user, verificationCode);

            default:
                return Result<VerificationResponse>.Fail(
                    VerificationResources.UnsupportedVerificationPurpose);
        }
    }
}
