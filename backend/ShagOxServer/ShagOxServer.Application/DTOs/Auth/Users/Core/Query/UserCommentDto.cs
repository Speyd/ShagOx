using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Query;

namespace ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
public sealed record UserCommentDto
(
    long Id,
    string? FirstName,
    string? LastName,
    string? UserName,

    AvatarDto? Avatar,

    long? CityId,

    DateTime? LastSeenAt
) : BaseDto(Id);
