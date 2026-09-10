using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Advertisements;
public class FavoriteConfiguration 
    : IEntityTypeConfiguration<Favorite>
{
    public void Configure(
        EntityTypeBuilder<Favorite> builder)
    {

        builder.HasOne(x => x.User)
               .WithMany(x => x.Favorites)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Advertisement)
               .WithMany(x => x.Favorites)
               .HasForeignKey(x => x.AdvertisementId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => 
            new { x.UserId, x.AdvertisementId })
            .IsUnique();
    }
}