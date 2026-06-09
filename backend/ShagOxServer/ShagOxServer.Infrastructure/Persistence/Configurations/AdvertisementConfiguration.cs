using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities;

namespace ShagOxServer.Infrastructure.Persistence.Configurations;

public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
{
    public void Configure(EntityTypeBuilder<Advertisement> builder)
    {
        builder.Property(x => x.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(x => x.Description)
               .IsRequired()
               .HasColumnType("text");

        builder.HasOne(x => x.Currency)
               .WithMany(x => x.Advertisements)
               .HasForeignKey(x => x.CurrencyId);

        builder.HasOne(x => x.Category)
               .WithMany(x => x.Advertisements)
               .HasForeignKey(x => x.CategoryId);
    }
}