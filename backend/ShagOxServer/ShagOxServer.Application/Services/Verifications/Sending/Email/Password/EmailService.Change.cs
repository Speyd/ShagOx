using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending.Email;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Application.Resources.Verifications.Password.Change;
using ShagOxServer.Application.Services.Verifications.Sending.Email.Processing.Models;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email;
public partial class EmailService
    : IEmailService
{
    private const string ChangePasswordEmailFileName = "Password/ChangePassword.cshtml";


    public async Task<Result<bool>> SendChangePasswordCodeAsync(
        string email,
        string code)
    {
        try
        {
            var dynamicModel = EmailViewModelFactory
                .ChangePasswordConfirmation(
                    code,
                    _codeOptions.ExpirationMinutes);

            var htmlBody = await _renderer.RenderAsync(
                ChangePasswordEmailFileName,
                dynamicModel);

            await SendEmailAsync(
                email,
                ChangePasswordEmail.Title,
                htmlBody);

            _logger.LogInformation(
                "Change password sent. email: {email}",
                email);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to email password.");

            return Result<bool>.Fail(
                VerificationResources.FailedToSendVerificationEmail);
        }
    }
}
