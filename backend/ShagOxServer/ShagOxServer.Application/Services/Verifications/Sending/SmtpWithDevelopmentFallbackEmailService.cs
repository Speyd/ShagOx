using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Sending;

/// <summary>
/// Development-only wrapper: tries to send the e-mail via SMTP and, if that fails
/// (e.g. the network blocks SMTP AUTH to Gmail), logs the verification code instead
/// so registration can still be completed locally.
/// </summary>
public class SmtpWithDevelopmentFallbackEmailService
    : IEmailService
{
    private readonly EmailService _smtp;
    private readonly ILogger<SmtpWithDevelopmentFallbackEmailService> _logger;


    public SmtpWithDevelopmentFallbackEmailService(
        EmailService smtp,
        ILogger<SmtpWithDevelopmentFallbackEmailService> logger)
    {
        _smtp = smtp;
        _logger = logger;
    }

    public async Task<Result<bool>> SendVerificationCodeAsync(
        string email,
        string code)
    {
        var result = await _smtp
            .SendVerificationCodeAsync(email, code);

        if (result.IsSuccess)
            return result;

        _logger.LogWarning(
            "SMTP sending failed (Development fallback). Verification code for {Email}: {Code}",
            email,
            code);

        return Result<bool>.Success(true);
    }
}
