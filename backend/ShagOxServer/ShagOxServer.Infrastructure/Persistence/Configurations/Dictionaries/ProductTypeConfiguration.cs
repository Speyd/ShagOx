using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Dictionaries;
public class ProductTypeConfiguration 
    : IEntityTypeConfiguration<ProductType>
{
    public void Configure(
        EntityTypeBuilder<ProductType> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Description)
            .HasColumnType("text")
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}