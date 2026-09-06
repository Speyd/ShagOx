using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Specification.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Specification.Translations;
public class ConditionTranslationConfiguration
    : IEntityTypeConfiguration<ConditionTranslation>
{
    public void Configure(
        EntityTypeBuilder<ConditionTranslation> builder)
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

        builder.HasIndex(x => new { x.Language, x.Name })
            .IsUnique();
    }
}