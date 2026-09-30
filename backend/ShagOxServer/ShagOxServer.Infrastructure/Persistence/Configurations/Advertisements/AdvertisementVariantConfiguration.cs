using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Advertisements;
public class AdvertisementVariantConfiguration
    : IEntityTypeConfiguration<AdvertisementVariant>
{
    public void Configure(
        EntityTypeBuilder<AdvertisementVariant> builder)
    {
        builder.Property(x => x.Price)
         .HasPrecision(18, 2)
         .IsRequired();

        builder.Property(x => x.PreviousPrice)
         .HasPrecision(18, 2);

        builder.Property(x => x.Stock)
            .IsRequired();

        builder.Property(x => x.Attributes)
            .HasColumnType("jsonb")
            .IsRequired();

        builder.HasOne(x => x.Advertisement)
               .WithMany(x => x.Variants)
               .HasForeignKey(x => x.AdvertisementId)
               .OnDelete(DeleteBehavior.Cascade);


        builder.HasIndex(x => x.AdvertisementId);
    }
}