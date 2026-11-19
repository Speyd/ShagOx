using ShagOxServer.Domain.Entities.Payments.Enum;
using System.Text.Json.Serialization;

namespace ShagOxServer.Application.DTOs.Payment;
public record WebhookDto
(
    string InvoiceId,
    string Status,
    long Amount,
    int Ccy,
    long FinalAmount,
    string? Reference,
    DateTime ModifiedDate
)
{
    [JsonIgnore]
    public PaymentStatus EnumStatus { get; set; }
}
