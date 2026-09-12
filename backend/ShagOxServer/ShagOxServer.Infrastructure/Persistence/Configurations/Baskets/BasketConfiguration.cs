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
        builder.HasOne(x => x.User)
               .WithOne(x => x.Basket)
               .HasForeignKey<Basket>(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId)
            .IsUnique();
    }
}