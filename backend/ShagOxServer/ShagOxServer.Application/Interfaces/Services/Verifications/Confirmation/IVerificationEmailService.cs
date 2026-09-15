using ShagOxServer.Application.DTOs.Verifications.VerificationEmails;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation;
public interface IVerificationEmailService
{
    Task<Result<bool>> VerifyAsync(
       VerificationEmailRequest request);
}