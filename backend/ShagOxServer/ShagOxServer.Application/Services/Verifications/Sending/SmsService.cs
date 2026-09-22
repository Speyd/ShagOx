using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Verifivations;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.EmailService;
using ShagOxServer.SharedKernel.Abstractions.Results;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ShagOxServer.Application.Services.Verifications.Sending;
public class SmsService
    : ISmsService
{
    private readonly SmsSettings _smsOptions;
    private readonly VerificationCodeSettings _codeOptions;

    private readonly ILogger<SmsService> _logger;


    public SmsService(
        IOptions<SmsSettings> smsOptions,
        IOptions<VerificationCodeSettings> codeOptions,
        ILogger<SmsService> logger)
    {
        _smsOptions = smsOptions.Value;
        _codeOptions = codeOptions.Value;
        _logger = logger;
    }

    public async Task<Result<bool>> SendVerificationCodeAsync(
        string phone,
        string code)
    {
        try
        {
            TwilioClient.Init(_smsOptions.AccountSID,
                _smsOptions.AuthToken
            );

            var body = string.Format(
                Emails.VerificationBody,
                code, _codeOptions.ExpirationMinutes);

            var from = _smsOptions.FromTwilio;

            var message = await MessageResource.CreateAsync(
               to: new PhoneNumber(phone),
               from: new PhoneNumber(from),
               body: body
            );

            return Result<bool>.Success(true);
        }
        catch(Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Failed to send verification SMS.");

            return Result<bool>.Fail(
                "Failed to send verification SMS.");
        }
    }
}