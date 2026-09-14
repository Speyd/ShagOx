namespace ShagOxServer.Application.Interfaces.Services.Verifications;
public interface IEmailService
{
    Task SendVerificationCodeAsync(
        string email,
        string code);
}