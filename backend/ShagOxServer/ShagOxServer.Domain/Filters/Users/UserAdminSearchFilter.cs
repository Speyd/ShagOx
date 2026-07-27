namespace ShagOxServer.Domain.Filters.Users;
public sealed record UserAdminSearchFilter
(
    string? FullName,
    string? Email,
    string? Phone,
    int? CityId,
    DateTime? RegisteredAfter,
    DateTime? ActiveAfter
);