using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Sending.Email;
public partial interface IEmailService
{
    Task<Result<bool>> SendVerificationCodeAsync(
        string email,
        string code);
}
