namespace ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
internal interface ISmsSerice
{
    Task SendVerificationCodeAsync(
        string phone,
        string code);
}