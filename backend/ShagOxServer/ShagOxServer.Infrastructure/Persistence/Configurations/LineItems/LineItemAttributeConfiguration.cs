using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.LineItems;
using ShagOxServer.Infrastructure.Persistence.Configurations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.LineItems;
public class LineItemAttributeConfiguration
    : IEntityTypeConfiguration<LineItemAttribute>
{
    public void Configure(
        EntityTypeBuilder<LineItemAttribute> builder)
    {
        builder.Property(x => x.Order)
            .IsRequired();

        builder.HasOne(x => x.AttributeDefinition)
               .WithOne(x => x.BasketAttribute)
               .HasForeignKey<LineItemAttribute>(x => x.AttributeDefinitionId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasUniqueActiveIndex(x => x.AttributeDefinitionId);
    }
}
