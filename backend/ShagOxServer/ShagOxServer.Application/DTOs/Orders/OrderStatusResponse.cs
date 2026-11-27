using ShagOxServer.Domain.Entities.Orders.Enum;

namespace ShagOxServer.Application.DTOs.Orders;
public sealed record OrderStatusResponse
(
    long Id,
    OrderStatus OrderStatus,
    DateTime ChangedAt
);
