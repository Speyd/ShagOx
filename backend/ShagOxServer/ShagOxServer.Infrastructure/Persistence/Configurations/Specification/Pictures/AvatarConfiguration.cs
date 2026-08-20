using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Specification.Pictures;
public class AvatarConfiguration 
    : IEntityTypeConfiguration<Avatar>
{
    public void Configure(EntityTypeBuilder<Avatar> builder)
    {
        builder.Property(x => x.Url)
               .IsRequired()
               .HasColumnType("text");

        builder.Property(x => x.PublicId)
               .IsRequired()
               .HasMaxLength(255);

        builder.HasOne(x => x.User)
            .WithOne(x => x.Avatar)
            .HasForeignKey<Avatar>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId)
            .IsUnique();
    }
}