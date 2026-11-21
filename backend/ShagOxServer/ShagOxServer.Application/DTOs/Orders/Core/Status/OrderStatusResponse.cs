using ShagOxServer.Domain.Entities.Orders.Enum;

namespace ShagOxServer.Application.DTOs.Orders.Core.Status;
public sealed record OrderStatusResponse
(
    long Id,
    OrderStatus OrderStatus,
    DateTime ChangedAt
);
