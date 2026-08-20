using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Location;


namespace ShagOxServer.Infrastructure.Persistence.Configurations.Location;
public class RegionConfiguration 
    : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(x => x.Name);
    }
}