namespace ShagOxServer.Domain.Filters.Auth.Users.Core;
public sealed record UserAdminSearchFilter
(
    string? FullName,
    string? UserName,
    string? Bio,
    string? Email,
    string? Phone,
    long? CityId,
    DateTime? RegisteredAfter,
    DateTime? ActiveAfter
) : BaseFilter();