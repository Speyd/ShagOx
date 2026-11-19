using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Payments;
public class PaymentWebhook : BaseEntity
{
    public string InvoiceId { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Payload { get; set; } = null!;

    public bool IsProcessed { get; set; }
    public string? ProcessingError { get; set; }

    public DateTime ReceivedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }

    public override string ToString()
        => $"InvoiceId: {InvoiceId}, Status: {Status}";
}
