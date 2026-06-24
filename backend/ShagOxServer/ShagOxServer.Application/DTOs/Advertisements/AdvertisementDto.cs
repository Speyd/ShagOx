
namespace ShagOxServer.Application.DTOs.Advertisements;

public sealed record AdvertisementDto
(
    int Id,
    string Title,
    int Price,
    string Currency,
    string CurrencyId,
    string Category,
    string CategoryId,
    int SellerId,
    int? BuyerId,
    List<string> Images,
    Dictionary<string, string> Properties,
    DateTime? SoldAt,
    DateTime CreatedAt
);