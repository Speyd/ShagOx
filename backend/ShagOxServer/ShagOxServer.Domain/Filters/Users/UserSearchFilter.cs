namespace ShagOxServer.Domain.Filters.Users;
public sealed record UserSearchFilter
(
    string? FullName,
    string? UserName,
    string? Bio,
    string? Email,
    string? Phone
);