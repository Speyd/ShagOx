namespace ShagOxServer.Application.DTOs.Orders.Core.Query.Email.Create;
public sealed record OrderEmailItem
(
    string Name,

    string? ImageUrl,

    decimal Price,

    int Quantity
);
