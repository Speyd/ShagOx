using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Payments;
using ShagOxServer.Infrastructure.Persistence.Configurations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Payments;
public class PaymentConfiguration
    : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(x => x.InvoiceId)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.PaymentUrl)
            .IsRequired(false)
            .HasMaxLength(2048);

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.ExpiresAt)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasUniqueActiveIndex(x => x.InvoiceId);

        builder.HasIndex(x => new
        {
            x.Status,
            x.ExpiresAt
        });
    }
}
