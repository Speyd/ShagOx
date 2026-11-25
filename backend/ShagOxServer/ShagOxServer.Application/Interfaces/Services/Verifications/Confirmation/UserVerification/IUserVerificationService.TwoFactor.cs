using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.DTOs.Verifications.Create;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation.UserVerification;
public partial interface IUserVerificationService
{
    Task<Result<VerificationResponse>> EnableTwoFactorAsync(
        User user,
        VerificationCode verificationCode);

    Task<Result<VerificationResponse>> DisableTwoFactorAsync(
        User user,
        VerificationCode verificationCode);

    Task<Result<VerificationResponse>> InitiateTwoFactorAsync(
        User user,
        VerificationCode verificationCode);
}
