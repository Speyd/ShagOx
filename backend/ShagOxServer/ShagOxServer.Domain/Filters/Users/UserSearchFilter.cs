namespace ShagOxServer.Domain.Filters.Users;
public sealed record UserSearchFilter
(
    string? FullName,
    string? Email,
    string? Phone
);