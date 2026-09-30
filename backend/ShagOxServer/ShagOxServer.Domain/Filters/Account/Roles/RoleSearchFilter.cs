namespace ShagOxServer.Domain.Filters.Auth.Roles;
public sealed record RoleSearchFilter
(
    string? Name
) : BaseFilter();