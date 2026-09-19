using ShagOxServer.Application.DTOs.Verifications;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation;
public interface IVerificationService
{
    Task<Result<bool>> VerifyAsync(
       VerificationRequest request);
}