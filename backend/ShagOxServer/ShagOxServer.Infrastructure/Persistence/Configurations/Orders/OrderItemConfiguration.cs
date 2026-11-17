using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Orders;
public class OrderItemConfiguration
    : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(
        EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.TotalPrice)
            .HasPrecision(18, 2)
            .IsRequired();


        builder.HasOne(x => x.Parent)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Advertisement)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.AdvertisementId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.AdvertisementVariant)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.AdvertisementVariantId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasIndex(x => x.ParentId);

        builder.HasIndex(x => x.AdvertisementId);

        builder.HasIndex(x => x.AdvertisementVariantId);
    }
}
