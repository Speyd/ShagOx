using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.Verifications.Sms;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.Services.Verifications.Sending.Phone;
public partial class SmsService
    : ISmsService
{
    public async Task<Result<bool>> SendPhoneChangeApprovalCodeAsync(
        string phone,
        string code)
    {
        var body = string.Format(
            SmsTemplatesResources.PhoneChangeApproval,
            code,
            _codeOptions.ExpirationMinutes);

        return await SendSmsAsync(phone, body);
    }
}
