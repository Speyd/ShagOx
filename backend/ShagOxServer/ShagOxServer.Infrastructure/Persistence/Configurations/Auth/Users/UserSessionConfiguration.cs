using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Infrastructure.Persistence.Configurations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Auth.Users;
public class UserSessionConfiguration
    : IEntityTypeConfiguration<UserSession>
{
    public void Configure(
        EntityTypeBuilder<UserSession> builder)
    {
        builder.Property(x => x.RefreshTokenHash)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
        .HasColumnType("timestamptz");

        builder.Property(x => x.ExpiresAt)
        .HasColumnType("timestamptz");

        builder.Property(x => x.RevokedAt)
            .HasColumnType("timestamptz");


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
    }
}
