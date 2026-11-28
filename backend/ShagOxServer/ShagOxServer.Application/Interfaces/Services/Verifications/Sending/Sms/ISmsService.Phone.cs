using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Sending.Sms;
public partial interface ISmsService
{
    Task<Result<bool>> SendPhoneChangeApprovalCodeAsync(
        string phone,
        string code);

    Task<Result<bool>> SendNewPhoneConfirmationCodeAsync(
        string phone,
        string code);
}
