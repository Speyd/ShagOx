using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Verifivations;
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

        using var client = new SmtpClient(smtpHost, smtpPort)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(
                smtpUser,
                new string(smtpPassword.Where(c => !char.IsWhiteSpace(c)).ToArray()))
        };

        var subject = Emails.VerificationSubject;

        var body = string.Format(
            Emails.VerificationBody,
            code, _codeOptions.ExpirationMinutes);
        
        using var message = new MailMessage
        {
            From = new MailAddress(smtpUser, "Marketly"),
            Subject = subject,
            Body = body,
            IsBodyHtml = false
        };

        if (!string.IsNullOrWhiteSpace(_emailOptions.From))
            message.ReplyToList.Add(new MailAddress(_emailOptions.From));

        message.To.Add(email);

        await client.SendMailAsync(message);
    }
}