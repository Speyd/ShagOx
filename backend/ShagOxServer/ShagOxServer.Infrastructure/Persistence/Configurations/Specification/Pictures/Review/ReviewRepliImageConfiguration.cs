using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.Infrastructure.Persistence.Configurations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Specification.Pictures.Review;
public class ReviewRepliImageConfiguration
    : IEntityTypeConfiguration<ReviewRepliImage>
{
    public void Configure(
        EntityTypeBuilder<ReviewRepliImage> builder)
    {
        builder.Property(x => x.Url)
               .IsRequired()
               .HasColumnType("text");

        builder.Property(x => x.PublicId)
               .IsRequired()
               .HasMaxLength(255);

        builder.HasOne(x => x.Owner)
            .WithMany(x => x.Pictures)
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasUniqueActiveIndex(x => 
            new { x.OwnerId, x.Order });
    }
}
