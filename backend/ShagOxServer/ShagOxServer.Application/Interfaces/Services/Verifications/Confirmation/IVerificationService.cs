using ShagOxServer.Application.DTOs.Verifications.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation;
public interface IVerificationService
{
    Task<Result<VerificationResponse>> VerifyAsync(
       VerificationRequest request);
}
