using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Application.Resources.Verifications.Email;
using ShagOxServer.Application.Services.Verifications.Sending.Email.Processing.Models;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Net;
using System.Net.Mail;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email;
public partial class EmailService
    : IEmailService
{
    private const string VerificationEmailFileName = "Verification/EmailVerification.cshtml";



    public async Task<Result<bool>> SendVerificationCodeAsync(
        string email,
        string code)
    {
        try
        {
            var model = EmailViewModelFactory
                .CreateVerificationEmail(
                    code,
                    _codeOptions.ExpirationMinutes);

            var htmlBody = await _renderer.RenderAsync(
                VerificationEmailFileName,
                model);

            await SendEmailAsync(
                email,
                EmailVerificationEmailResources.Title,
                htmlBody);

            _logger.LogInformation(
                "Verification email sent successfully.");

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send verification email. Email: {Email}",
                email);

            return Result<bool>.Fail(
                VerificationResources.FailedToSendVerificationEmail);
        }
    }

}
