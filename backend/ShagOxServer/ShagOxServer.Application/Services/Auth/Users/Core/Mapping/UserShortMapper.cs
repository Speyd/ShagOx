using ShagOxServer.Application.DTOs.Auth.Users.Core;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Auth.Users.Core.Mapping;
public static class UserShortMapper
{
    public static UserShortDto ToDto(
        User user)
    {
        return new UserShortDto(
            user.Id,
            user.FirstName,
            user.LastName,
            user.UserName,
            user.Bio,
            user.Phone,
            user.Email,
            user.Avatar?.Id,
            user.CityId,
            user.City?.Code ?? "Unknown city code",
            user.LastSeenAt
        );
    }
}