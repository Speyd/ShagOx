using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Configurations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Advertisements;
public class FavoriteConfiguration 
    : IEntityTypeConfiguration<Favorite>
{
    public void Configure(
        EntityTypeBuilder<Favorite> builder)
    {

        builder.HasOne(x => x.Owner)
               .WithMany(x => x.Favorites)
               .HasForeignKey(x => x.OwnerId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Advertisement)
               .WithMany(x => x.Favorites)
               .HasForeignKey(x => x.AdvertisementId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasUniqueActiveIndex(x => 
            new { x.OwnerId, x.AdvertisementId });
    }
}
