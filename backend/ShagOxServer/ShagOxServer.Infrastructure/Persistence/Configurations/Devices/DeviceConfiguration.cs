using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Devices;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Devices;
public class DeviceConfiguration
    : IEntityTypeConfiguration<Device>
{
    public void Configure(
        EntityTypeBuilder<Device> builder)
    {
        builder.Property(x => x.DeviceName)
            .HasMaxLength(100);

        builder.Property(x => x.Status)
            .HasConversion<string>();

        builder.Property(x => x.DeviceType)
            .HasMaxLength(50);

        builder.Property(x => x.OperatingSystem)
            .HasMaxLength(100);

        builder.Property(x => x.DeviceIdentifier)
            .HasMaxLength(45);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("timestamptz");

        builder.Property(x => x.LastActiveAt)
            .HasColumnType("timestamptz");

        builder.Property(x => x.BlockedAt)
            .HasColumnType("timestamptz")
            .IsRequired(false);

        builder.Property(x => x.BlockedUntil)
            .HasColumnType("timestamptz")
            .IsRequired(false);


        builder.HasIndex(x => x.DeviceIdentifier)
            .IsUnique();
    }
}
