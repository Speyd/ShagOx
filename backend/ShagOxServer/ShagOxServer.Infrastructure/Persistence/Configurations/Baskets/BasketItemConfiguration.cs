using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Baskets;

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

        builder.HasOne(x => x.Basket)
               .WithMany(x => x.BasketItems)
               .HasForeignKey(x => x.BasketId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Advertisement)
               .WithMany(x => x.BasketItems)
               .HasForeignKey(x => x.AdvertisementId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => 
                new { x.BasketId, x.AdvertisementId })
            .IsUnique();
    }
}