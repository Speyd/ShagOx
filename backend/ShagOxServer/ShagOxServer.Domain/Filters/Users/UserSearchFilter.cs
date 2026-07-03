namespace ShagOxServer.Domain.Filters.Users;
public sealed record UserSearchFilter
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}