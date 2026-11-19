using System.Text.Json.Serialization;

namespace ShagOxServer.Infrastructure.ExternalServices.Payments.Providers.MonoBank.Models;
public sealed record MonoWebhook
(
    [property: JsonPropertyName("invoiceId")]
    string InvoiceId,

    [property: JsonPropertyName("status")]
    string Status,

    [property: JsonPropertyName("amount")]
    long Amount,

    [property: JsonPropertyName("ccy")]
    int Ccy,

    [property: JsonPropertyName("finalAmount")]
    long FinalAmount,

    [property: JsonPropertyName("reference")]
    string? Reference,

    [property: JsonPropertyName("modifiedDate")]
    DateTime ModifiedDate
);
