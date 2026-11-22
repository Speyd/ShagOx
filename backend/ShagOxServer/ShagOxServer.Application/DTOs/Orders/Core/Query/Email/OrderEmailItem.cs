namespace ShagOxServer.Application.DTOs.Orders.Core.Query.Email;
public sealed record OrderEmailItem
(
    string Name,

    string? ImageUrl,

    decimal Price,

    int Quantity
);
