namespace ShagOxServer.Application.DTOs.Advertisement.Add;
public sealed record AdvertisementAddResponse(
    bool Success,
    string Message,
    DateTime CreatedAt
);