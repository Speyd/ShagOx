using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Infrastructure.Persistence.Configurations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Deliveries;
public class DeliveryProviderConfiguration
    : IEntityTypeConfiguration<DeliveryProvider>
{
    public void Configure(
        EntityTypeBuilder<DeliveryProvider> builder)
    {
        builder.Property(x => x.Code)
           .HasMaxLength(50)
           .IsRequired();

        builder.HasUniqueActiveIndex(x => x.Code);
    }
}
