using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Infrastructure.Persistence.Configurations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Specification.Pictures;
public class CategoryImageConfiguratio
    : IEntityTypeConfiguration<CategoryImage>
{
    public void Configure(
        EntityTypeBuilder<CategoryImage> builder)
    {
        builder.Property(x => x.Url)
               .IsRequired()
               .HasColumnType("text");

        builder.Property(x => x.PublicId)
               .IsRequired()
               .HasMaxLength(255);

        builder.HasOne(x => x.Owner)
            .WithOne(x => x.CategoryImage)
            .HasForeignKey<CategoryImage>(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasUniqueActiveIndex(x => x.OwnerId);
    }
}
