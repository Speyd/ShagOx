namespace ShagOxServer.Domain.Filters.Auth.Users;
public sealed record UserSearchFilter
(
    string? FullName,
    string? UserName,
    string? Bio,
    string? Email,
    string? Phone
) : BaseFilter();