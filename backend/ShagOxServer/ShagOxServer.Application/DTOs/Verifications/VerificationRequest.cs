namespace ShagOxServer.Application.DTOs.Verifications;
public record VerificationRequest(
    long? UserId,
    string Code
);