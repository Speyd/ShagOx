using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using ShagOxServer.Application.Interfaces.Services.Verifications;
using System.Net;
using System.Net.Mail;

namespace ShagOxServer.Application.Services.Verifications;
public class EmailService 
    : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly IStringLocalizer<Emails> _localizer;

    public EmailService(
        IConfiguration configuration,
        IStringLocalizer<Emails> localizer)
    {
        _configuration = configuration;
        _localizer = localizer;
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

        var subject = _localizer["VerificationSubject"];

        var body = string.Format(
            _localizer["VerificationBody"],
            code);

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