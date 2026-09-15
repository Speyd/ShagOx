using ShagOxServer.Domain.Entities.Verifications;

namespace ShagOxServer.Application.Services.Verifications.Enum;
public record VerificationResult(
    VerificationCodeResult Result,
    VerificationCode? Code
);