namespace ShagOxServer.Application.DTOs.Auth.Users.Statistics;

/// <summary>Numbers for the profile "Dashboard" tab: the user as a seller and as a buyer.</summary>
public sealed record UserStatisticsDto(
    SellerStatisticsDto Seller,
    BuyerStatisticsDto Buyer);

public sealed record SellerStatisticsDto(
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
    IReadOnlyList<TopAdvertisementDto> TopAdvertisements);

public sealed record BuyerStatisticsDto(
    int OrdersCount,
    int ActiveOrdersCount,
    IReadOnlyList<MoneyAmountDto> Spent,
    int FavoritesCount,
    int ReviewsWritten);

public sealed record GradeCountDto(
    int Grade,
    int Count);

public sealed record MoneyAmountDto(
    string CurrencyCode,
    string Symbol,
    decimal Amount);

public sealed record TopAdvertisementDto(
    long Id,
    string Title,
    string? PictureUrl,
    int SoldUnits,
    int Favorites,
    int Reviews,
    double? AverageGrade);
