using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
public interface IEmailService
{
    Task<Result<bool>> SendVerificationCodeAsync(
        string email,
        string code);
}