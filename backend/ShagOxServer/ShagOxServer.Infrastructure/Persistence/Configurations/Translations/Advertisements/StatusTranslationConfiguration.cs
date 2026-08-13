using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Localizations.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Translations.Advertisements;
public class StatusTranslationConfiguration 
    : IEntityTypeConfiguration<StatusTranslation>
{
    public void Configure(EntityTypeBuilder<StatusTranslation> builder)
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

        builder.HasOne(x => x.Status)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.Language, x.Name})
            .IsUnique();
    }
}