using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
public sealed record UserShortDto
(
    long Id,
    EntityStatus EntityStatus,
    string? FirstName,
    string? LastName,
    string? UserName,
    string? Bio,
    string? Phone,
    string? Email,
    long? AvatarId,
    long? CityId,
    DateTime? LastSeenAt
) : BaseDto(Id, EntityStatus);
