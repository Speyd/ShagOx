using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Specification.Pictures;

public class ImageConfiguration 
    : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.Property(x => x.Url)
               .IsRequired()
               .HasColumnType("text");

        builder.Property(x => x.PublicId)
               .IsRequired()
               .HasMaxLength(255);

        builder.HasOne(x => x.Advertisement)
           .WithMany(x => x.Images)
           .HasForeignKey(x => x.AdvertisementId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.AdvertisementId, x.Order })
               .IsUnique();
    }
}