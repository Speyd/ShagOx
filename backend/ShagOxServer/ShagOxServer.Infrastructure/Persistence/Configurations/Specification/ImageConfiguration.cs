using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Specification;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.Property(x => x.Url)
               .IsRequired()
               .HasColumnType("text");

        builder.HasOne(x => x.Advertisement)
           .WithMany(x => x.Images)
           .HasForeignKey(x => x.AdvertisementId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.AdvertisementId, x.Order })
               .IsUnique();
    }
}