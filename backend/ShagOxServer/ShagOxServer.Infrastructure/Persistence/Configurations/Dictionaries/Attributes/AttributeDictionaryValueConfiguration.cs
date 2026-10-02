using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Dictionaries.Attributes;
public class AttributeDictionaryValueConfiguration
    : IEntityTypeConfiguration<AttributeDictionaryValue>
{
    public void Configure(
        EntityTypeBuilder<AttributeDictionaryValue> builder)
    {
        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Value)
            .HasColumnType("text");

        builder.HasOne(x => x.Dictionary)
            .WithMany(x => x.Values)
            .HasForeignKey(x => x.DictionaryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.DictionaryId, x.Code })
             .IsUnique();
    }
}