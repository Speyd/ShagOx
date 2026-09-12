using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Baskets;
public class BasketAttributeConfiguration
    : IEntityTypeConfiguration<BasketAttribute>
{
    public void Configure(
        EntityTypeBuilder<BasketAttribute> builder)
    {
        builder.Property(x => x.Order)
            .IsRequired();

        builder.HasOne(x => x.AttributeDefinition)
               .WithOne(x => x.BasketAttribute)
               .HasForeignKey<BasketAttribute>(x => x.AttributeDefinitionId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.AttributeDefinitionId )
            .IsUnique();
    }
}