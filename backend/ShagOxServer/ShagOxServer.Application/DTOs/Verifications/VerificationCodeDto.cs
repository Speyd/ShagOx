using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Entities.Verifications.Enum;

namespace ShagOxServer.Application.DTOs.Verifications;
public sealed record VerificationCodeDto
(
    long Id,
    long UserId,
    DateTime ExpiresAt,
    DateTime? UsedAt,
    int Attempts,
    DateTime CreatedAt,
    DateTime? InvalidatedAt,
    VerificationCodePurpose Purpose
) : BaseDto(Id);