namespace ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
public interface ISmsService
{
    Task SendVerificationCodeAsync(
        string phone,
        string code);
}