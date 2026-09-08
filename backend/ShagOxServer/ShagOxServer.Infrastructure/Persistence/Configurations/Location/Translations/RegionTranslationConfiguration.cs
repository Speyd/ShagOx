using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Location.Translations;
public class RegionTranslationConfiguration
    : IEntityTypeConfiguration<RegionTranslation>
{
    public void Configure(
        EntityTypeBuilder<RegionTranslation> builder)
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