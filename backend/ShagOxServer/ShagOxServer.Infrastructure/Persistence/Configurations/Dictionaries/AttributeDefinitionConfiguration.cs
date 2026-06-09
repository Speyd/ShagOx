using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Dictionaries;

public class AttributeDefinitionConfiguration : IEntityTypeConfiguration<AttributeDefinition>
{
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public string Key { get; set; } = null!;
    public string Type { get; set; } = null!;
    public bool Required { get; set; }

    public int? Min { get; set; }
    public int? Max { get; set; }

    public void Configure(EntityTypeBuilder<AttributeDefinition> builder)
    {
        builder.Property(x => x.Key)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Attributes)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.CategoryId, x.Key })
             .IsUnique();
    }
}
