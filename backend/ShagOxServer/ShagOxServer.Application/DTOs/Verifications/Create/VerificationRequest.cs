namespace ShagOxServer.Application.DTOs.Verifications.Create;
public record VerificationRequest(
    long? UserId,
    string Code
);