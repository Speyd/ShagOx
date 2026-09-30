using ShagOxServer.Application.DTOs.Verifications;
using ShagOxServer.Domain.Entities.Verifications;

namespace ShagOxServer.Application.Services.Verifications.Codes.Mapping;
public static class VerificationCodeMapper
{
    public static VerificationCodeDto ToDto(
       VerificationCode code)
    {
        return new VerificationCodeDto(
            code.Id,
            code.UserId,
            code.ExpiresAt,
            code.UsedAt,
            code.Attempts,
            code.CreatedAt,
            code.InvalidatedAt,
            code.Purpose
        );
    }
}
