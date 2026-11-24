namespace ShagOxServer.Application.DTOs.Verifications.Create;
public record VerificationResultResponse
(
    bool IsSuccessful
) : VerificationResponse;
