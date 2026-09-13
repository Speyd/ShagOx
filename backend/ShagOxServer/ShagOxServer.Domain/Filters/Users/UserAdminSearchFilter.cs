namespace ShagOxServer.Domain.Filters.Users;
public sealed record UserAdminSearchFilter
(
    string? FullName,
    string? UserName,
    string? Bio,
    string? Email,
    string? Phone,
    int? CityId,
    DateTime? RegisteredAfter,
    DateTime? ActiveAfter
);