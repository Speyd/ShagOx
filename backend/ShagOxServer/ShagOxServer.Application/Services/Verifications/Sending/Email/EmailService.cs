using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Builders;
using ShagOxServer.Application.Common.Options.Verifivations;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Services.Verifications.Sending.Email.Processing;

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
}
