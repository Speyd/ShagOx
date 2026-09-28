namespace ShagOxServer.Domain.Filters.Auth.UserRoles;
public sealed record UserRoleSearchFilter
(
    long? UserId,
    long? RoleId
) : BaseFilter();