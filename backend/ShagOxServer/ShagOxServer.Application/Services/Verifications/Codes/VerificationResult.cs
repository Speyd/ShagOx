using ShagOxServer.Application.Services.Verifications.Enum;
using ShagOxServer.Domain.Entities.Verifications;

namespace ShagOxServer.Application.Services.Verifications.Codes;
public record VerificationResult(
    VerificationCodeResult Result,
    VerificationCode? Code
);