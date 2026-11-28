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
        builder.Property(x => x.Status)
            .HasConversion<string>();

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


        builder.HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey(x => x.DeviceId)
            .OnDelete(DeleteBehavior.Cascade);



        builder.HasUniqueActiveIndex(x => new
        {
            x.OwnerId,
            x.DeviceId
        });

        builder.HasIndex(x => x.DeviceId);

        builder.HasIndex(x => x.BlockedUntil)
            .HasFilter("\"Status\" = 'Blocked' AND \"BlockedUntil\" IS NOT NULL");
    }
}
