using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Sending.Email;
public partial interface IEmailService
{
    Task<Result<bool>> SendNewEmailConfirmationCodeAsync(
        string email,
        string code);

    Task<Result<bool>> SendEmailChangeApprovalCodeAsync(
        string email,
        string newEmail,
        string code);
}
