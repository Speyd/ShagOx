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

    private Task<Result<VerificationResponse>> ApplyVerification(
        User user,
        VerificationCode? code)
    {
        return code?.Purpose switch
        {
            VerificationCodePurpose.RegistrationEmail =>
                _userVerifyService.ConfirmEmailAsync(user, code),

            VerificationCodePurpose.RegistrationPhone =>
                _userVerifyService.ConfirmPhoneAsync(user, code),

            VerificationCodePurpose.ChangeNewEmail =>
                _userVerifyService.ChangeNewEmailAsync(user, code),

            VerificationCodePurpose.ChangeCurrentEmail =>
                _userVerifyService.ChangeCurrentEmailAsync(user, code),

            VerificationCodePurpose.ChangeNewPhone =>
                _userVerifyService.ChangeNewPhoneAsync(user, code),

            VerificationCodePurpose.ChangeCurrentPhone =>
                _userVerifyService.ChangeCurrentPhoneAsync(user, code),

            VerificationCodePurpose.ChangePasswordEmail or
            VerificationCodePurpose.ChangePasswordPhone or
            VerificationCodePurpose.ResetPasswordEmail or
            VerificationCodePurpose.ResetPasswordPhone =>
                _userVerifyService.ChangePasswordAsync(user, code),

            VerificationCodePurpose.EnableTwoFactorEmail or
            VerificationCodePurpose.EnableTwoFactorPhone =>
                _userVerifyService.EnableTwoFactorAsync(user, code),

            VerificationCodePurpose.DisableTwoFactorEmail or
            VerificationCodePurpose.DisableTwoFactorPhone =>
                _userVerifyService.DisableTwoFactorAsync(user, code),

            VerificationCodePurpose.InitiateTwoFactorEmail or
            VerificationCodePurpose.InitiateTwoFactorPhone =>
                _userVerifyService.InitiateTwoFactorAsync(user, code),

            _ => Task.FromResult(
                Result<VerificationResponse>.Fail(
                    VerificationResources.UnsupportedVerificationPurpose))
        };
    }
}
