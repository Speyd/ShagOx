using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Domain.Entities.Payments;
public class Payment 
    : BaseEntity
{
    public long OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public string InvoiceId { get; set; } = null!;
    public string? PaymentUrl { get; set; }

    public decimal Amount { get; set; }

    public long CurrencyId { get; set; }
    public Currency Currency { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public PaymentStatus Status { get; set; }


    public override string ToString()
    {
        return $"PaymentUrl: {PaymentUrl}";
    }
}
