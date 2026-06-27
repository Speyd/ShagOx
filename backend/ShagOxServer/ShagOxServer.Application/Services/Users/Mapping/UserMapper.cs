using ShagOxServer.Application.DTOs.Roles;
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
            MapRoles(user.UserRoles),
            user.LastSeenAt,
            user.RegisteredAt
        );
    }

    private static List<RoleDto> MapRoles(List<UserRole> userRoles)
    {
        return userRoles
            .Where(x => x.Role != null)
            .Select(x => new RoleDto(
                x.Role!.Id,
                x.Role.Name,
                x.Role.Description))
            .ToList();
    }
}