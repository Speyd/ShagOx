namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin.Users.Dtos;
internal sealed record SellerCount(
    long OwnerId, 
    int Count
);
