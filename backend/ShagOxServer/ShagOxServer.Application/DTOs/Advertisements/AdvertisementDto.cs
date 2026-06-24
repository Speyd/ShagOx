
namespace ShagOxServer.Application.DTOs.Advertisements;

public sealed record AdvertisementDto
(
    int Id,
    string Title,
    string Description,
    int Price,
    int PreviousPrice,
    int CurrencyId,
    string Currency,
    int CategoryId,
    string Category,
    int SellerId,
    int? BuyerId,
    List<string> Images,
    Dictionary<string, string> Properties,
    DateTime? SoldAt,
    DateTime CreatedAt
);