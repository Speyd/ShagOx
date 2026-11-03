using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Infrastructure.Persistence.Configurations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Dictionaries;
public class ProductTypeConfiguration 
    : IEntityTypeConfiguration<ProductType>
{
    public void Configure(
        EntityTypeBuilder<ProductType> builder)
    {
        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Description)
            .HasColumnType("text")
            .IsRequired();

        builder.HasUniqueActiveIndex(x => x.Code);
    }
}
