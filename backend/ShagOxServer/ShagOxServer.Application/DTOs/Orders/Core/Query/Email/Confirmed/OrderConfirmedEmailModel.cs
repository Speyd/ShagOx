namespace ShagOxServer.Application.DTOs.Orders.Core.Query.Email.Confirmed;
public sealed record OrderConfirmedEmailModel
(
    string OrderNumber,
    string OrderUrl,
    string CustomerEmail
);
