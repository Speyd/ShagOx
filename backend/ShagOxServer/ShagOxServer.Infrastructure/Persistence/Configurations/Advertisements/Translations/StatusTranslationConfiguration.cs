using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Advertisements.Translations;
public class StatusTranslationConfiguration 
    : IEntityTypeConfiguration<StatusTranslation>
{
    public void Configure(
        EntityTypeBuilder<StatusTranslation> builder)
    {
        builder.Property(x => x.Language)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnType("text")
            .IsRequired();

        builder.HasOne(x => x.Translatable)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.TranslatableId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.Language, x.Name})
            .IsUnique();
    }
}