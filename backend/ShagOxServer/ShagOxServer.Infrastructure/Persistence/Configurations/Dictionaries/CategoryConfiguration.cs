using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Dictionaries;

public class CategoryConfiguration 
    : IEntityTypeConfiguration<Category>
{
    public void Configure(
        EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.ProductTypeId)
            .IsRequired();

        builder.HasMany(x => x.Attributes)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.ProductType)
            .WithMany(x => x.Categories)
            .HasForeignKey(x => x.ProductTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.ProductTypeId);
    }
}