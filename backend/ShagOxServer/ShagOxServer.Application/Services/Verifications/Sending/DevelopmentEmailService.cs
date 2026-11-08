using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Sending;
public class DevelopmentEmailService
    : IEmailService
{
    private readonly ILogger<DevelopmentEmailService> _logger;


    public DevelopmentEmailService(
        ILogger<DevelopmentEmailService> logger)
    {
        _logger = logger;
    }

    public Task<Result<bool>> SendVerificationCodeAsync(
        string email,
        string code)
    {
        _logger.LogWarning(
            "SMTP is not configured. Verification code for {Email}: {Code}",
            email,
            code);

        return Task.FromResult(Result<bool>.Success(true));
    }
}
