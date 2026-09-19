namespace ShagOxServer.Application.DTOs.Verifications;
public record VerificationRequest(
    int? UserId,
    string Code
);