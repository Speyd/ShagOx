using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation.UserVerification;
public partial interface IUserVerificationService
{
    Task<Result<bool>> ChangePassword(
        User user,
        VerificationCode verificationCode);
}
