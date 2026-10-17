using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Comments;
public class ReviewCommentConfiguration
    : IEntityTypeConfiguration<ReviewComment>
{
    public void Configure(
        EntityTypeBuilder<ReviewComment> builder)
    {
        builder.Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.Grade)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
           .IsRequired()
           .HasColumnType("timestamp with time zone")
           .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("timestamp with time zone");


        builder.HasOne(x => x.CommentableEntity)
            .WithMany(x => x.ReviewComments)
            .HasForeignKey(x => x.CommentableEntityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasIndex(x => x.CommentableEntityId);

        builder.HasIndex(x => x.OwnerId);

        builder.HasIndex(x => new
        {
            x.CommentableEntityId,
            x.CreatedAt
        });


        builder.ToTable(table =>
            table.HasCheckConstraint(
                "CK_ReviewComments_Grade",
                "\"Grade\" BETWEEN 1 AND 5"));
    }
}
