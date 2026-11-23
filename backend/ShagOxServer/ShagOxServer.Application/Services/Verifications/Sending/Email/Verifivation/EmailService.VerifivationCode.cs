using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Application.Resources.Verifications.Email;
using ShagOxServer.Application.Services.Verifications.Sending.Email.Processing;
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
            var smtpHost = _emailOptions.SmtpHost
                ?? throw new InvalidOperationException("SMTP host is not configured.");

            var smtpPort = _emailOptions.SmtpPort;

            var smtpUser = _emailOptions.Username
                ?? throw new InvalidOperationException("SMTP username is not configured.");

            var smtpPassword = _emailOptions.Password
                ?? throw new InvalidOperationException("SMTP password is not configured.");

            var fromEmail = _emailOptions.From
                ?? throw new InvalidOperationException("Sender email is not configured.");


            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(
                    smtpUser,
                    smtpPassword)
            };

            var dynamicModel = EmailViewModelFactory
                .Create(code, _codeOptions.ExpirationMinutes);

            var htmlBody = await _renderer.RenderAsync(
                VerificationEmailFileName,
                dynamicModel);

            var subject = EmailVerificationEmailResources.Title;

            using var message = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };

            message.To.Add(email);

            await client.SendMailAsync(message);

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
