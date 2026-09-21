using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Verifivations;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.EmailService;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ShagOxServer.Application.Services.Verifications.Sending;
public class SmsService
    : ISmsService
{
    private readonly SmsSettings _smsOptions;
    private readonly VerificationCodeSettings _codeOptions;


    public SmsService(
        IOptions<SmsSettings> smsOptions,
         IOptions<VerificationCodeSettings> codeOptions)
    {
        _smsOptions = smsOptions.Value;
        _codeOptions = codeOptions.Value;
    }

    public async Task SendVerificationCodeAsync(
        string phone,
        string code)
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
    }
}