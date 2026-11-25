using ShagOxServer.Application.DTOs.Auth.Users.Statistics.Shared;

namespace ShagOxServer.Application.DTOs.Auth.Users.Statistics.Buyer;
public sealed record BuyerStatisticsDto
(
    int OrdersCount,
    int ActiveOrdersCount,
    IReadOnlyList<MoneyAmountDto> Spent,
    int FavoritesCount,
    int ReviewsWritten
);
