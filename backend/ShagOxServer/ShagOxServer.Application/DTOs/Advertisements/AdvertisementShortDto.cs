namespace ShagOxServer.Application.DTOs.Advertisements;
public sealed record AdvertisementShortDto
(
    int Id,
    string Title,
    string Description,
    int Price,
    int PreviousPrice,
    int CurrencyId,
    int CategoryId,
    int SellerId,
    int? BuyerId,
    List<int> ImageIds,
    Dictionary<string, string> Properties,
    DateTime? SoldAt,
    DateTime CreatedAt
);