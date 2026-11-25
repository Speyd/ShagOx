using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Auth.Users;
public class UserSessionConfiguration
    : IEntityTypeConfiguration<UserSession>
{
    public void Configure(
        EntityTypeBuilder<UserSession> builder)
    {
        builder.Property(x => x.RefreshTokenHash)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(x => x.RevocationReason)
            .HasConversion<string>()
            .IsRequired(false);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("timestamptz");

        builder.Property(x => x.ExpiresAt)
            .HasColumnType("timestamptz");

        builder.Property(x => x.RevokedAt)
            .HasColumnType("timestamptz");

        builder.Property(x => x.LastUsedAt)
            .HasColumnType("timestamptz");


        builder.HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey(x => x.DeviceId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasIndex(x => x.RefreshTokenHash)
            .IsUnique();

        builder.HasIndex(x => x.DeviceId);

        builder.HasIndex(x => new
        {
            x.OwnerId,
            x.RevokedAt
        });
    }
}
