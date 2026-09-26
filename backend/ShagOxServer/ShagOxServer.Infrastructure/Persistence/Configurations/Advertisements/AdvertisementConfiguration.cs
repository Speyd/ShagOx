using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Advertisements;
public class AdvertisementConfiguration 
    : IEntityTypeConfiguration<Advertisement>
{
    public void Configure(
        EntityTypeBuilder<Advertisement> builder)
    {
        builder.Property(x => x.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(x => x.Description)
               .IsRequired()
               .HasColumnType("text");

        builder.Property(x => x.Attributes)
            .HasColumnType("jsonb")
            .IsRequired();

        builder.HasOne(x => x.Currency)
            .WithMany(x => x.Advertisements)
            .HasForeignKey(x => x.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Condition)
            .WithMany(x => x.Advertisements)
            .HasForeignKey(x => x.ConditionId)
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

        builder.HasOne(x => x.Status)
              .WithMany(x => x.Advertisements)
              .HasForeignKey(x => x.StatusId)
              .OnDelete(DeleteBehavior.Restrict);


        builder.HasIndex(x => x.SellerId);
        builder.HasIndex(x => x.CategoryId);
        builder.HasIndex(x => x.CurrencyId);
        builder.HasIndex(x => x.ConditionId);
        builder.HasIndex(x => x.StatusId);

        builder.HasIndex(x => x.CreatedAt);
        builder.HasIndex(x => x.Popularity);

        builder.HasIndex(x => new { x.CategoryId, x.CreatedAt });
        builder.HasIndex(x => new { x.CategoryId, x.Popularity });
        builder.HasIndex(x => new { x.CategoryId, x.ConditionId, x.CreatedAt });
        builder.HasIndex(x => new { x.SellerId, x.CreatedAt });
    }
}