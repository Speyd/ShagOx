using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Infrastructure.Persistence.Configurations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Dictionaries.Attributes;
public class AttributeDefinitionConfiguration 
    : IEntityTypeConfiguration<AttributeDefinition>
{
    public void Configure(
        EntityTypeBuilder<AttributeDefinition> builder)
    {
        builder.Property(x => x.Key)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Attributes)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasUniqueActiveIndex(x =>
            new { x.CategoryId, x.Key });
    }
}
