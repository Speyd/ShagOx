namespace ShagOxServer.Domain.Entities.Orders.Enum;
public enum OrderStatus
{
    Pending = 1,
    Confirmed = 2,
    Processing = 3,
    ReadyForShipment = 4,
    Shipped = 5,
    InTransit = 6,
    Delivered = 7,
    Completed = 8,

    Cancelled = 9,
    Rejected = 10,
    Refunded = 11
}
