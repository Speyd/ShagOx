using Microsoft.Extensions.Configuration;
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
    private readonly IConfiguration _configuration;

    private readonly VerificationCodeSettings _options;

    public EmailService(
        IConfiguration configuration,
        IOptions<VerificationCodeSettings> options)
    {
        _configuration = configuration;
        _options = options.Value;
    }

    public async Task SendVerificationCodeAsync(
        string email,
        string code)
    {
        var smtpHost = _configuration["Email:SmtpHost"]
            ?? throw new InvalidOperationException("SMTP host is not configured.");

        var smtpPort = int.Parse(
            _configuration["Email:SmtpPort"] ?? "587");

        var smtpUser = _configuration["Email:Username"]
            ?? throw new InvalidOperationException("SMTP username is not configured.");

        var smtpPassword = _configuration["Email:Password"]
            ?? throw new InvalidOperationException("SMTP password is not configured.");

        var fromEmail = _configuration["Email:From"]
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
            code, _options.ExpirationMinutes);
        
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