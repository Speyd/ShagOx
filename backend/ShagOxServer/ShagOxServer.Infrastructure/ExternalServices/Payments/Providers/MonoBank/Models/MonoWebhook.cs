using ShagOxServer.Application.DTOs.Payment;
using System.Text.Json.Serialization;

namespace ShagOxServer.Infrastructure.ExternalServices.Payments.Providers.MonoBank.Models;

public sealed record MonoWebhook 
    : WebhookDto
{
    public MonoWebhook(
        string invoiceId,
        string status,
        long amount,
        int ccy,
        long finalAmount,
        string? reference,
        DateTime modifiedDate)
        : base(
            invoiceId,
            status,
            amount,
            ccy,
            finalAmount,
            reference,
            modifiedDate)
    {
    }

    [JsonPropertyName("invoiceId")]
    public new string InvoiceId => base.InvoiceId;

    [JsonPropertyName("status")]
    public new string Status => base.Status;

    [JsonPropertyName("amount")]
    public new long Amount => base.Amount;

    [JsonPropertyName("ccy")]
    public new int Ccy => base.Ccy;

    [JsonPropertyName("finalAmount")]
    public new long FinalAmount => base.FinalAmount;

    [JsonPropertyName("reference")]
    public new string? Reference => base.Reference;

    [JsonPropertyName("modifiedDate")]
    public new DateTime ModifiedDate => base.ModifiedDate;
}
