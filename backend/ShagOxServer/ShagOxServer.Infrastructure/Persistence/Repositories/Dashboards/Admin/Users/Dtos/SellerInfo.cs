namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin.Users.Dtos;
internal sealed record SellerInfo
(
    long Id,
    string? UserName,
    string? AvatarUrl
);
