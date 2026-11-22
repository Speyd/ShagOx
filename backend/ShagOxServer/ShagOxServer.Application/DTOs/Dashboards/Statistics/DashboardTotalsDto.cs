namespace ShagOxServer.Application.DTOs.Dashboards.Statistics;
public sealed record DashboardTotalsDto
(
    int Users,
    int UsersLast30Days,
    int Advertisements,
    int AdvertisementsLast30Days,
    int SoldAdvertisements,
    int TotalStock,
    int Reviews,
    double AverageRating,
    int Categories,
    int ProductTypes,
    int AttributeDictionaries,
    int Cities
);
