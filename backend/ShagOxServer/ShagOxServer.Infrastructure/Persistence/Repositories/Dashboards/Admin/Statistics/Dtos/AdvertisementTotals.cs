namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin.Statistics.Dtos;
internal sealed record AdvertisementTotals
(
    int Total,
    int Last30Days,
    int Sold,
    int TotalStock
);
