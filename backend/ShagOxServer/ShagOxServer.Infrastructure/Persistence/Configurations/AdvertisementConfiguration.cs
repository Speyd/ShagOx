using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShagOxServer.Domain.Entities;
using System.Text.Json;

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

        var converter = new ValueConverter<Dictionary<string, string>, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions?)null)!
        );

        builder.Property(x => x.Properties)
            .HasConversion(converter)
            .HasColumnType("jsonb")
            .IsRequired();

        builder.HasOne(x => x.Currency)
               .WithMany(x => x.Advertisements)
               .HasForeignKey(x => x.CurrencyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Category)
               .WithMany(x => x.Advertisements)
               .HasForeignKey(x => x.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Seller)
               .WithMany(x => x.SoldAdvertisements)
               .HasForeignKey(x => x.SellerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Buyer)
               .WithMany(x => x.BoughtAdvertisements)
               .HasForeignKey(x => x.BuyerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.SellerId);
        builder.HasIndex(x => x.CategoryId);
        builder.HasIndex(x => x.CurrencyId);

        builder.HasIndex(x => x.Price);
        builder.HasIndex(x => x.CreatedAt);
        builder.HasIndex(x => x.Popularity);

        builder.HasIndex(x => new { x.CategoryId, x.Price });
        builder.HasIndex(x => new { x.CategoryId, x.CreatedAt });
        builder.HasIndex(x => new { x.CategoryId, x.Popularity });
    }
}