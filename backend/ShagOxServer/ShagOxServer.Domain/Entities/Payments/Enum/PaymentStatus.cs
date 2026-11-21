namespace ShagOxServer.Domain.Entities.Payments.Enum;
public enum PaymentStatus
{
    Pending = 0,
    Processing = 1,
    Succeeded = 2,
    Failed = 3,
    Cancelled = 4,
    Expired = 5,
    Refunded = 6
}
