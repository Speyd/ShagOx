using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Builders;
using ShagOxServer.Application.Common.Options.Verifivations;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Services.Verifications.Sending.Email.Processing;
using System.Net;
using System.Net.Mail;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email;
public partial class EmailService 
    : IEmailService
{
    private readonly EmailOptions _emailOptions;
    private readonly VerificationCodeOptions _codeOptions;

    private readonly EmailTemplateRenderer _renderer;
    private readonly OrderUrlBuilder _urlBuilder;

    private readonly ILogger<EmailService> _logger;


    public EmailService(
        IOptions<EmailOptions> emailOptions,
        IOptions<VerificationCodeOptions> codeOptions,
        EmailTemplateRenderer renderer,
        OrderUrlBuilder urlBuilder,
        ILogger<EmailService> logger)
    {
        _emailOptions = emailOptions.Value;
        _codeOptions = codeOptions.Value;
        _renderer = renderer;
        _urlBuilder = urlBuilder;
        _logger = logger;
    }


    private async Task SendEmailAsync(
        string email,
        string subject,
        string htmlBody)
    {
        var smtpHost = _emailOptions.SmtpHost
            ?? throw new InvalidOperationException(
                "SMTP host is not configured.");

        var smtpUser = _emailOptions.Username
            ?? throw new InvalidOperationException(
                "SMTP username is not configured.");

        var smtpPassword = _emailOptions.Password
            ?? throw new InvalidOperationException(
                "SMTP password is not configured.");

        var fromEmail = _emailOptions.From
            ?? throw new InvalidOperationException(
                "Sender email is not configured.");

        using var client = new SmtpClient(
            smtpHost,
            _emailOptions.SmtpPort)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(
                smtpUser,
                smtpPassword)
        };

        using var message = new MailMessage
        {
            From = new MailAddress(fromEmail),
            Subject = subject,
            Body = htmlBody,
            IsBodyHtml = true
        };

        message.To.Add(email);

        await client.SendMailAsync(message);
    }

}
