using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Payments;
using ShagOxServer.Infrastructure.Persistence.Configurations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Payments;
public class PaymentWebhookConfiguration
    : IEntityTypeConfiguration<PaymentWebhook>
{
    public void Configure(
        EntityTypeBuilder<PaymentWebhook> builder)
    {
        builder.Property(x => x.InvoiceId)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Payload)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(x => x.IsProcessed)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.ProcessingError)
            .IsRequired(false)
            .HasColumnType("text");

        builder.Property(x => x.ReceivedAt)
            .IsRequired()
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW()");

        builder.Property(x => x.ProcessedAt)
            .IsRequired(false)
            .HasColumnType("timestamp with time zone");

        builder.HasUniqueActiveIndex(
            x => x.InvoiceId,
            "\"Status\" = 'success'");
    }
}
