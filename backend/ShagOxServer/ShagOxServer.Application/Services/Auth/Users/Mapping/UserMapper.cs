using ShagOxServer.Application.DTOs.Auth.Roles;
using ShagOxServer.Application.DTOs.Auth.Users;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars;
using ShagOxServer.Application.Services.Location.Cities.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Mapping;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Auth.Users.Mapping;
public static class UserMapper
{
    public static UserDto ToDto(
        User user)
    {
        return new UserDto(
            user.Id,
            user.Surname,
            user.Name,
            user.Phone,
            user.Email,
            MapAvatar(user.Avatar),
            CityMapper.ToDto(user.City),
            MapRoles(user.UserRoles),
            user.LastSeenAt,
            user.RegisteredAt
        );
    }

    private static List<RoleDto> MapRoles(
        List<UserRole> userRoles)
    {
        if(userRoles is null)
            return new List<RoleDto>();

        return userRoles
         .Where(x => x.Role != null)
         .Select(x => new RoleDto(
             x.Role!.Id,
             x.Role.Name,
             x.Role.Description))
         .ToList();
    }

    private static AvatarDto? MapAvatar(
        Avatar? avatar)
    {
        return avatar is null
            ? null
            : AvatarMapper.ToDto(avatar);
    }
}