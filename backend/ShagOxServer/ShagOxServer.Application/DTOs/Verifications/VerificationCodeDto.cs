using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Verifications.Enum;

namespace ShagOxServer.Application.DTOs.Verifications;
public sealed record VerificationCodeDto
(
    long Id,
    EntityStatus EntityStatus,
    long UserId,
    DateTime ExpiresAt,
    DateTime? UsedAt,
    int Attempts,
    DateTime CreatedAt,
    DateTime? InvalidatedAt,
    VerificationCodePurpose Purpose
) : BaseDto(Id, EntityStatus);
