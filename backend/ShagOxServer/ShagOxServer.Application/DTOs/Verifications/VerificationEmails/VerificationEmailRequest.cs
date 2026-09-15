namespace ShagOxServer.Application.DTOs.Verifications.VerificationEmails;
public record VerificationEmailRequest(
    int? UserId,
    string Code
);