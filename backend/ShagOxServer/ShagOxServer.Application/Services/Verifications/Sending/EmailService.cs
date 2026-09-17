using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.EmailService;
using System.Net;
using System.Net.Mail;

namespace ShagOxServer.Application.Services.Verifications.Sending;
public class EmailService 
    : IEmailService
{
    private readonly EmailSettings _emailOptions;
    private readonly VerificationCodeSettings _codeOptions;


    public EmailService(
        IOptions<EmailSettings> emailOptions,
        IOptions<VerificationCodeSettings> codeOptions)
    {
        _emailOptions = emailOptions.Value;
        _codeOptions = codeOptions.Value;

    }

    public async Task SendVerificationCodeAsync(
        string email,
        string code)
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

        var subject = Emails.VerificationSubject;

        var body = string.Format(
            Emails.VerificationBody,
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
    }
}