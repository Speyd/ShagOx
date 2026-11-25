using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Application.Resources.Verifications.TwoFactor.Enable;
using ShagOxServer.Application.Services.Verifications.Sending.Email.Processing.Models;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email;
public partial class EmailService
    : IEmailService
{
    private const string EnableTwoFactorFileName = "TwoFactor/EnableTwoFactor.cshtml";


    public async Task<Result<bool>> SendEnableTwoFactorCodeAsync(
        string email,
        string code)
    {
        try
        {
            var model = EmailViewModelFactory
                .CreateEnableTwoFactor(
                    code,
                    _codeOptions.ExpirationMinutes);

            var htmlBody = await _renderer.RenderAsync(
                EnableTwoFactorFileName,
                model);

            await SendEmailAsync(
                email,
                EnableTwoFactorResources.Title,
                htmlBody);

            _logger.LogInformation(
                "Two-factor authentication email sent successfully.");

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send two-factor authentication email. Email: {Email}",
                email);

            return Result<bool>.Fail(
                VerificationResources.FailedToSendVerificationEmail);
        }
    }

}
