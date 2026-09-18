using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation;
public interface IUserVerificationService
{
    public Result<bool> ConfirmEmail(
        User user,
        VerificationCode verificationCode);

    public Result<bool> ChangeEmail(
        User user,
        VerificationCode verificationCode);

    public Result<bool> ConfirmPhone(
        User user,
        VerificationCode verificationCode);

    public Result<bool> ChangePhone(
        User user,
        VerificationCode verificationCode);

    public Result<bool> ChangePassword(
        User user,
        VerificationCode verificationCode);
}