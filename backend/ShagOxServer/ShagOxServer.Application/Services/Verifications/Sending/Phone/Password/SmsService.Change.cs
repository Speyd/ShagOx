using ShagOxServer.Application.Interfaces.Services.Verifications.Sending.Sms;
using ShagOxServer.Application.Resources.Verifications.Sms;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Sending.Phone;
public partial class SmsService
    : ISmsService
{
    public async Task<Result<bool>> SendChangePasswordCodeAsync(
        string phone,
        string code)
    {
        var body = string.Format(
            SmsTemplatesResources.ChangePasswordBody,
            code,
            _codeOptions.ExpirationMinutes);

        return await SendSmsAsync(phone, body);
    }
}
