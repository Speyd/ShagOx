using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Verifivations;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.Messaging.Email;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Net;
using System.Net.Mail;

namespace ShagOxServer.Application.Services.Verifications.Sending;
public class EmailService 
    : IEmailService
{
    private readonly EmailSettings _emailOptions;
    private readonly VerificationCodeSettings _codeOptions;

    private readonly ILogger<EmailService> _logger;


    public EmailService(
        IOptions<EmailSettings> emailOptions,
        IOptions<VerificationCodeSettings> codeOptions,
        ILogger<EmailService> logger)
    {
        _emailOptions = emailOptions.Value;
        _codeOptions = codeOptions.Value;
        _logger = logger;
    }

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

            var subject = EmailTemplatesResources.VerificationSubject;

            var body = string.Format(
                EmailTemplatesResources.VerificationBody,
                code, _codeOptions.ExpirationMinutes);

            using var message = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
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