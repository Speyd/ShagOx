using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Users.Mapping;
public static class UserMapper
{
    public static UserDto ToDto(User user)
    {
        return new UserDto(
            user.Id,
            user.Surname,
            user.Name,
            user.Phone,
            user.Email,
            user.Avatar,
            user.CityId,
            user.City?.Name ?? "Unknown city name",
            user.UserRoles.Select(r => r.RoleId).ToList(),
            user.UserRoles.Select(r => r.Role?.Name ?? "Unknown role").ToList(),
            user.LastSeenAt,
            user.RegisteredAt
        );
    }
}