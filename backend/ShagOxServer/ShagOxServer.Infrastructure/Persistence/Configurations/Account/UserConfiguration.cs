using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Account;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(35);

        builder.Property(x => x.Surname)
               .IsRequired()
               .HasMaxLength(35);

        builder.Property(x => x.Phone)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(254);

        builder.Property(x => x.PasswordHash)
                 .HasColumnType("text");

        builder.Property(x => x.Avatar)
                 .HasColumnType("text");

        builder.Property(x => x.LastSeenAt)
                .HasColumnType("timestamptz");

        builder.Property(x => x.RegisteredAt)
                .HasColumnType("timestamptz");

        builder.HasMany(x => x.UserRoles)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);
    }
}