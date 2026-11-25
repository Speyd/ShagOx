namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin.Statistics.Dtos;
internal sealed record UserTotals
(
    int Total,
    int Last30Days
);
