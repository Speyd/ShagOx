using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Infrastructure.Persistence.Configurations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Location;
public class RegionConfiguration 
    : IEntityTypeConfiguration<Region>
{
    public void Configure(
        EntityTypeBuilder<Region> builder)
    {
        builder.Property(x => x.Code)
            .HasMaxLength(50)
            .IsRequired();


        builder.HasUniqueActiveIndex(x => x.Code);
    }
}
