using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Specification;
public class ConditionConfiguration 
    : IEntityTypeConfiguration<Condition>
{
    public void Configure(
        EntityTypeBuilder<Condition> builder)
    {
        builder.Property(x => x.Code)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(x => x.Code)
            .IsUnique();
    }
}