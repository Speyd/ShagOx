using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Infrastructure.Persistence.Configurations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Orders;

public class OrderConfiguration
    : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.OrderNumber)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasMaxLength(35)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(35)
            .IsRequired();

        builder.Property(x => x.Phone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(254)
            .IsRequired();

        builder.Property(x => x.Comment)
            .HasMaxLength(1000)
            .IsRequired(false);



        builder.Property(x => x.Subtotal)
            .HasPrecision(18, 2);

        builder.Property(x => x.DeliveryPrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.TotalPrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.TotalWeight)
            .HasPrecision(18, 2);



        builder.Property(x => x.DeliveryType)
            .HasConversion<string>()
            .IsRequired();



        builder.HasOne(x => x.DeliveryProvider)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.DeliveryProviderId)
            .OnDelete(DeleteBehavior.Restrict);



        builder.Property(x => x.RecipientCityExternalId)
            .HasMaxLength(100)
            .IsRequired(false);


        builder.Property(x => x.DeliveryPointExternalId)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.DeliveryPointNumber)
            .HasMaxLength(35)
            .IsRequired(false);

        builder.Property(x => x.DeliveryPointName)
            .HasMaxLength(150)
            .IsRequired(false);

        builder.Property(x => x.DeliveryPointAddress)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(x => x.DeliveryPointType)
            .HasConversion<string>()
            .IsRequired(false);



        builder.Property(x => x.RecipientStreetExternalId)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.RecipientBuildingNumber)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.RecipientFlatNumber)
            .HasMaxLength(20)
            .IsRequired(false);




        builder.Property(x => x.DeliveryTrackingNumber)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.DeliveryTrackingUrl)
            .HasMaxLength(2048)
            .IsRequired(false);



        builder.Property(x => x.PaymentMethod)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.PaymentStatus)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.PaymentTransactionId)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(x => x.PaymentProvider)
            .HasMaxLength(100)
            .IsRequired(false);


        builder.Property(x => x.OrderStatus)
            .HasConversion<string>()
            .IsRequired();



        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(x => x.ConfirmedAt)
            .IsRequired(false)
            .HasColumnType("timestamp with time zone");

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false)
            .HasColumnType("timestamp with time zone");

        builder.Property(x => x.CancelledAt)
            .IsRequired(false)
            .HasColumnType("timestamp with time zone");


        builder.Property(x => x.ShippedAt)
            .IsRequired(false)
            .HasColumnType("timestamp with time zone");

        builder.Property(x => x.DeliveredAt)
            .IsRequired(false)
            .HasColumnType("timestamp with time zone");

        builder.Property(x => x.EstimatedDeliveryAt)
            .IsRequired(false)
            .HasColumnType("timestamp with time zone");

        builder.Property(x => x.PaidAt)
            .IsRequired(false)
            .HasColumnType("timestamp with time zone");



        builder.HasOne(x => x.Owner)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Currency)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);



        builder.HasIndex(x => new
        {
            x.OwnerId,
            x.CreatedAt
        });

        builder.HasIndex(x => new
        {
            x.OrderStatus,
            x.CreatedAt
        });

        builder.HasIndex(x => new
        {
            x.PaymentMethod,
            x.PaymentStatus
        });

        builder.HasIndex(x => new
        {
            x.PaymentProvider,
            x.PaymentTransactionId
        });

        builder.HasIndex(x => x.DeliveryProviderId);

        builder.HasIndex(x => x.DeliveryType);

        builder.HasUniqueActiveIndex(x => x.OrderNumber);
    }
}
