using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities;

namespace ShagOxServer.Infrastructure.Persistence.Configurations;
public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasOne(x => x.Region)
               .WithMany(x => x.Cities)
               .HasForeignKey(x => x.RegionId);
    }
}
