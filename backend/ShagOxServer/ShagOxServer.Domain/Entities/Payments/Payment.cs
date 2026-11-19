using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Payments.Enum;

namespace ShagOxServer.Domain.Entities.Payments;
public class Payment 
    : BaseEntity
{
    public long OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public string InvoiceId { get; set; } = null!;
    public string? PaymentUrl { get; set; }

    public decimal Amount { get; set; }

    public DateTime ExpiresAt { get; set; }

    public PaymentStatus Status { get; set; }


    public override string ToString()
    {
        return $"PaymentUrl: {PaymentUrl}";
    }
}
