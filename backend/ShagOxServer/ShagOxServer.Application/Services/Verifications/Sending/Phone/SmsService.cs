using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Verifivations;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.SharedKernel.Abstractions.Results;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ShagOxServer.Application.Services.Verifications.Sending.Phone;
public partial class SmsService
    : ISmsService
{
    private readonly SmsOptions _smsOptions;
    private readonly VerificationCodeOptions _codeOptions;

    private readonly ILogger<SmsService> _logger;


    public SmsService(
        IOptions<SmsOptions> smsOptions,
        IOptions<VerificationCodeOptions> codeOptions,
        ILogger<SmsService> logger)
    {
        _smsOptions = smsOptions.Value;
        _codeOptions = codeOptions.Value;
        _logger = logger;
    }

    private async Task<Result<bool>> SendSmsAsync(
        string phone,
        string body)
    {
        try
        {
            TwilioClient.Init(
                _smsOptions.AccountSID,
                _smsOptions.AuthToken);

            await MessageResource.CreateAsync(
                to: new PhoneNumber(phone),
                from: new PhoneNumber(_smsOptions.FromTwilio),
                body: body);

            _logger.LogInformation(
                "SMS sent successfully.");

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send SMS. Phone: {Phone}",
                phone);

            return Result<bool>.Fail(
                VerificationResources.FailedToSendVerificationSms);
        }
    }
}
