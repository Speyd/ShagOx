using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
public interface ISmsService
{
    Task<Result<bool>> SendVerificationCodeAsync(
        string phone,
        string code);
}