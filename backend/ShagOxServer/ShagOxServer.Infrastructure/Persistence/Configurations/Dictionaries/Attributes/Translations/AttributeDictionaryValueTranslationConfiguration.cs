using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Dictionaries.Attributes.Translations;
public class AttributeDictionaryValueTranslationConfiguration
    : IEntityTypeConfiguration<AttributeDictionaryValueTranslation>
{
    public void Configure(
        EntityTypeBuilder<AttributeDictionaryValueTranslation> builder)
    {
        builder.Property(x => x.Language)
           .HasMaxLength(30)
           .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(x => x.Translatable)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.TranslatableId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasIndex(x => new { x.TranslatableId, x.Language })
            .IsUnique();

        builder.HasIndex(x => x.Name);

        builder.HasIndex(x => new { x.Language, x.Name });
    }
}