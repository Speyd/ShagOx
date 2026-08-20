using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Account;

public class UserConfiguration 
    : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Name)
               .HasMaxLength(35);

        builder.Property(x => x.Surname)
               .HasMaxLength(35);

        builder.Property(x => x.Phone)
               .HasMaxLength(20);
                

        builder.Property(x => x.Email)
               .HasMaxLength(254);

        builder.Property(x => x.PasswordHash)
                 .HasColumnType("text");

        builder.Property(x => x.LastSeenAt)
                .HasColumnType("timestamptz");

        builder.Property(x => x.RegisteredAt)
                .HasColumnType("timestamptz");

        builder.HasOne(x => x.City)
               .WithMany(x => x.Users)
               .HasForeignKey(x => x.CityId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.UserRoles)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.CityId);

        builder.HasIndex(x => x.Email)
               .IsUnique()
               .HasFilter("\"Email\" IS NOT NULL");


        builder.HasIndex(x => x.Phone)
               .IsUnique()
               .HasFilter("\"Phone\" IS NOT NULL");
    }
}