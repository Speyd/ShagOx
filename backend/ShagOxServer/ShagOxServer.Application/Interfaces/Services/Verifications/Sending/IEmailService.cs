namespace ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
public interface IEmailService
{
    Task SendVerificationCodeAsync(
        string email,
        string code);
}