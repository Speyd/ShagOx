using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Infrastructure.Persistence.Configurations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Baskets;
public class BasketItemConfiguration
    : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(
        EntityTypeBuilder<BasketItem> builder)
    {
        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasDefaultValue(1);

        builder.HasOne(x => x.Parent)
               .WithMany(x => x.BasketItems)
               .HasForeignKey(x => x.ParentId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Advertisement)
               .WithMany(x => x.BasketItems)
               .HasForeignKey(x => x.AdvertisementId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.AdvertisementVariant)
               .WithMany(x => x.BasketItems)
               .HasForeignKey(x => x.AdvertisementVariantId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasUniqueActiveIndex(
            x => new { x.ParentId, x.AdvertisementId },
            "\"AdvertisementVariantId\" IS NULL");

        builder.HasUniqueActiveIndex(
            x => new { x.ParentId, x.AdvertisementVariantId },
            "\"AdvertisementVariantId\" IS NOT NULL");
    }
}
