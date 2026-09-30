using ShagOxServer.Domain.Entities.Verifications.Enum;

namespace ShagOxServer.Domain.Filters.Verifications.VerificationCodes;
public sealed record VerificationCodeSearchFilter
(
    long? UserId,
    DateTime? ExpiresAt,
    DateTime? UsedAt,
    int? Attempts,
    DateTime? CreatedAt,
    DateTime? InvalidatedAt,
    VerificationCodePurpose Purpose
) : BaseFilter();