using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Verifications;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Verifications;
public class VerificationCodeConfiguration
    : IEntityTypeConfiguration<VerificationCode>
{
    public void Configure(
        EntityTypeBuilder<VerificationCode> builder)
    {
        builder.Property(x => x.CodeHash)
           .IsRequired()
           .HasColumnType("text");

        builder.Property(x => x.ExpiresAt)
            .HasColumnType("timestamptz");

        builder.Property(x => x.InvalidatedAt)
            .HasColumnType("timestamptz");

        builder.Property(x => x.UsedAt)
            .HasColumnType("timestamptz");

        builder.Property(x => x.CreatedAt)
            .HasColumnType("timestamptz");

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
    }
}