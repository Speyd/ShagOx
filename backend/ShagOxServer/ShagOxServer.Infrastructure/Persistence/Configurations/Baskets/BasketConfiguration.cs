using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Baskets;

public class BasketConfiguration
    : IEntityTypeConfiguration<Basket>
{
    public void Configure(
        EntityTypeBuilder<Basket> builder)
    {
        builder.HasOne(x => x.Owner)
            .WithOne(x => x.Basket)
            .HasForeignKey<Basket>(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.OwnerId)
            .IsUnique();
    }
}
