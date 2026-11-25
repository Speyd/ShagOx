namespace ShagOxServer.Application.DTOs.Orders.Core.Query.Email.Create;
public sealed record OrderPaidEmailModel
(
    string OrderNumber,

    string CustomerName,

    string CustomerEmail,

    string CurrencyCode,
    string CurrencySymbol,

    decimal Subtotal,

    decimal DeliveryPrice,

    decimal TotalPrice,

    string OrderUrl,

    IReadOnlyList<OrderEmailItem> Items
);
