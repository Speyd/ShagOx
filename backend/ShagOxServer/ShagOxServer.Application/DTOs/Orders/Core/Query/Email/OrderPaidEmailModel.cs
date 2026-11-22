namespace ShagOxServer.Application.DTOs.Orders.Core.Query.Email;
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
)
{
    public string PaymentSuccess { get; init; } = string.Empty;
    public string ThankYou { get; init; } = string.Empty;
    public string OrderTitle { get; init; } = string.Empty;
    public string Greeting { get; init; } = string.Empty;
    public string YourProducts { get; init; } = string.Empty;
    public string Quantity { get; init; } = string.Empty;
    public string Products { get; init; } = string.Empty;
    public string Delivery { get; init; } = string.Empty;
    public string Total { get; init; } = string.Empty;
    public string ViewOrder { get; init; } = string.Empty;
    public string Footer { get; init; } = string.Empty;
};
