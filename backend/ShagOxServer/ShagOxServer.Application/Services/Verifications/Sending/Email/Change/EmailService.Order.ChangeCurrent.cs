using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.Verifications.ChangeEmail.ChangeCurrency;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Application.Services.Verifications.Sending.Email.Processing.Models;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email;
public partial class EmailService
    : IEmailService
{
    private const string EmailChangeApprovalEmailFileName = "Order/EmailChangeApproval.cshtml";


    public async Task<Result<bool>> SendEmailChangeApprovalCodeAsync(
        string email,
        string newEmail,
        string code)
    {
        try
        {
            var dynamicModel = EmailViewModelFactory
                .CreateChangeApprovalEmailConfirmation(
                    code,
                    _codeOptions.ExpirationMinutes,
                    email,
                    newEmail);

            var htmlBody = await _renderer.RenderAsync(
                EmailChangeApprovalEmailFileName,
                dynamicModel);

            await SendEmailAsync(
                email,
                EmailChangeApprovalResources.Title,
                htmlBody);

            _logger.LogInformation(
                "Change email approval sent. email: {email}",
                email);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to email change approval.");

            return Result<bool>.Fail(
                VerificationResources.FailedToSendVerificationEmail);
        }
    }
}
