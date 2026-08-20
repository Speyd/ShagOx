using ShagOxServer.Application.DTOs.Auth.Users;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Auth.Users.Mapping;
public static class UserShortMapper
{
    public static UserShortDto ToDto(
        User user)
    {
        return new UserShortDto(
            user.Id,
            user.Surname,
            user.Name,
            user.Phone,
            user.Email,
            user.Avatar?.Id,
            user.CityId,
            user.City?.Name ?? "Unknown city name",
            user.LastSeenAt
        );
    }
}