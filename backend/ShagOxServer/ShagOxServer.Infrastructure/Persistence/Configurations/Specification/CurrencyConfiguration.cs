using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Specification;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.Property(x => x.Name)
           .IsRequired()
           .HasMaxLength(50);

        builder.Property(x => x.Code)
               .IsRequired()
               .HasMaxLength(3);

        builder.Property(x => x.Symbol)
               .IsRequired()
               .HasMaxLength(5);

        builder.HasIndex(x => x.Code)
               .IsUnique();
    }
}