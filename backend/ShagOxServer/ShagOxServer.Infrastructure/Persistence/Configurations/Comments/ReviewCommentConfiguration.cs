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

        builder.HasOne(x => x.CommentableEntity)
            .WithMany(x => x.ReviewComments)
            .HasForeignKey(x => x.CommentableEntityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(table =>
            table.HasCheckConstraint(
                "CK_ReviewComments_Grade",
                "\"Grade\" BETWEEN 1 AND 5"));
    }
}
