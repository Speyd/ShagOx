using ShagOxServer.Application.DTOs.Auth.Users.Statistics.Buyer;
using ShagOxServer.Application.DTOs.Auth.Users.Statistics.Seller;

namespace ShagOxServer.Application.DTOs.Auth.Users.Statistics;
public sealed record UserStatisticsDto
(
    SellerStatisticsDto Seller,
    BuyerStatisticsDto Buyer
);
