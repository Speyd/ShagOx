using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.Verifications.ChangeEmail.CreateNew;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Application.Services.Verifications.Sending.Email.Processing.Models;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email;
public partial class EmailService
    : IEmailService
{
    private const string NewEmailFileName = "Change/NewEmailConfirmation.cshtml";


    public async Task<Result<bool>> SendNewEmailConfirmationCodeAsync(
        string email,
        string code)
    {
        try
        {
            var dynamicModel = EmailViewModelFactory
                .CreateNewEmailConfirmation(
                    code, 
                    _codeOptions.ExpirationMinutes);

            var htmlBody = await _renderer.RenderAsync(
                NewEmailFileName,
                dynamicModel);

            await SendEmailAsync(
                email,
                CreateNewEmailResources.Title,
                htmlBody);

            _logger.LogInformation(
                "New email sent. email: {email}",
                email);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to new email.");

            return Result<bool>.Fail(
                VerificationResources.FailedToSendVerificationEmail);
        }
    }
}
