using System.Text.Json.Serialization;

namespace ShagOxServer.Application.DTOs.Payment;
public sealed record PaymentResponse
(
    [property: JsonPropertyName("amount")]
    decimal Amount,

    [property: JsonPropertyName("invoiceId")]
    string InvoiceId,

    [property: JsonPropertyName("pageUrl")]
    string PaymentUrl
);
