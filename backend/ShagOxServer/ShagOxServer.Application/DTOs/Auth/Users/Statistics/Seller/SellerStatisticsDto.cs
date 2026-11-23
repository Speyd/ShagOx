using ShagOxServer.Application.DTOs.Auth.Users.Statistics.Shared;

namespace ShagOxServer.Application.DTOs.Auth.Users.Statistics.Seller;
public sealed record SellerStatisticsDto
(
    int ActiveAdvertisements,
    int OutOfStockAdvertisements,
    int UnitsInStock,
    int ReviewsCount,
    double? AverageGrade,
    IReadOnlyList<GradeCountDto> Grades,
    int FavoritesCount,
    int SalesCount,
    int SoldUnits,
    IReadOnlyList<MoneyAmountDto> Revenue,
    IReadOnlyList<TopAdvertisementDto> TopAdvertisements
);
