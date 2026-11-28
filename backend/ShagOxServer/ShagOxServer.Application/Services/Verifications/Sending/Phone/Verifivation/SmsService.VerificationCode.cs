using ShagOxServer.Application.Interfaces.Services.Verifications.Sending.Sms;
using ShagOxServer.Application.Resources.Verifications.Sms;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Sending.Phone;
public partial class SmsService
    : ISmsService
{
    public async Task<Result<bool>> SendVerificationCodeAsync(
        string phone,
        string code)
    {
        var body = string.Format(
            SmsTemplatesResources.VerificationBody,
            code,
            _codeOptions.ExpirationMinutes);

        return await SendSmsAsync(phone, body);
    }
}
