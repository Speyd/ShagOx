using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Query;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
public sealed record UserCommentDto
(
    long Id,
    EntityStatus EntityStatus,
    string? FirstName,
    string? LastName,
    string? UserName,
    AvatarDto? Avatar,
    long? CityId,
    DateTime? LastSeenAt
) : BaseDto(Id, EntityStatus);
