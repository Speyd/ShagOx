using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Infrastructure.Persistence.Configurations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Dictionaries.Attributes;
public class AttributeDictionaryConfiguration
    : IEntityTypeConfiguration<AttributeDictionary>
{
    public void Configure(
        EntityTypeBuilder<AttributeDictionary> builder)
    {
        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(x => x.Attributes)
            .WithOne(x => x.Dictionary)
            .HasForeignKey(x => x.DictionaryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasUniqueActiveIndex(x => x.Code);
    }
}
