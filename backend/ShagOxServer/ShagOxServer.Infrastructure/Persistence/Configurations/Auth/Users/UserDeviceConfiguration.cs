using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Infrastructure.Persistence.Configurations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Auth.Users;
public class UserDeviceConfiguration
    : IEntityTypeConfiguration<UserDevice>
{
    public void Configure(
        EntityTypeBuilder<UserDevice> builder)
    {
        builder.Property(x => x.DeviceName)
            .HasMaxLength(100);

        builder.Property(x => x.DeviceType)
            .HasMaxLength(50);

        builder.Property(x => x.Browser)
            .HasMaxLength(100);

        builder.Property(x => x.OperatingSystem)
            .HasMaxLength(100);

        builder.Property(x => x.IpAddress)
            .HasMaxLength(45);

        builder.Property(x => x.DeviceIdentifier)
            .HasMaxLength(45);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("timestamptz");

        builder.Property(x => x.LastActiveAt)
            .HasColumnType("timestamptz");


        builder.HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);



        builder.HasUniqueActiveIndex(x => new
        {
            x.OwnerId,
            x.DeviceIdentifier
        });
    }
}
