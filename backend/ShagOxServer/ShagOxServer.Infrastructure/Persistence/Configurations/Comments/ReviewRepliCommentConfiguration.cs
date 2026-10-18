using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Comments;
public class ReviewRepliCommentConfiguration
    : IEntityTypeConfiguration<ReviewRepliComment>
{
    public void Configure(
        EntityTypeBuilder<ReviewRepliComment> builder)
    {
        builder.Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
           .IsRequired()
           .HasColumnType("timestamp with time zone")
           .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("timestamp with time zone");



        builder.HasOne(x => x.CommentableEntity)
            .WithMany(x => x.Replies)
            .HasForeignKey(x => x.CommentableEntityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.ParentReply)
            .WithMany(x => x.Replies)
            .HasForeignKey(x => x.ParentReplyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasIndex(x => x.CommentableEntityId);

        builder.HasIndex(x => x.ParentReplyId);

        builder.HasIndex(x => x.OwnerId);


        builder.HasIndex(x => new
        {
            x.CommentableEntityId,
            x.CreatedAt
        });

        builder.HasIndex(x => new
        {
            x.ParentReplyId,
            x.CreatedAt
        });
    }
}
