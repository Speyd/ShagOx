using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending.Email;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Application.Resources.Verifications.Password.Reset;
using ShagOxServer.Application.Services.Verifications.Sending.Email.Processing.Models;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email;
public partial class EmailService
    : IEmailService
{
    private const string ResetPasswordEmailFileName = "Password/ResetPassword.cshtml";


    public async Task<Result<bool>> SendResetPasswordCodeAsync(
        string email,
        string code)
    {
        try
        {
            var dynamicModel = EmailViewModelFactory
                .ResetPasswordConfirmation(
                    code,
                    _codeOptions.ExpirationMinutes);

            var htmlBody = await _renderer.RenderAsync(
                ResetPasswordEmailFileName,
                dynamicModel);

            await SendEmailAsync(
                email,
                ResetPasswordEmail.Title,
                htmlBody);

            _logger.LogInformation(
                "Reset password sent. email: {email}",
                email);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to password reset approval.");

            return Result<bool>.Fail(
                VerificationResources.FailedToSendVerificationEmail);
        }
    }
}
