using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Configurations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Advertisements;
public class StatusConfiguration 
    : IEntityTypeConfiguration<Status>
{
    public void Configure(
        EntityTypeBuilder<Status> builder)
    {
        builder.Property(x => x.Code)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasUniqueActiveIndex(x => x.Code);
    }
}
