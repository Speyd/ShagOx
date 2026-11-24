using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation;
public interface IUserVerificationService
{
    public Task<Result<bool>> ConfirmEmail(
        User user,
        VerificationCode verificationCode);

    public Task<Result<bool>> ChangeEmail(
        User user,
        VerificationCode verificationCode);

    public Task<Result<bool>> ConfirmPhone(
        User user,
        VerificationCode verificationCode);

    public Task<Result<bool>> ChangePhone(
        User user,
        VerificationCode verificationCode);

    public Task<Result<bool>> ChangePassword(
        User user,
        VerificationCode verificationCode);
}
