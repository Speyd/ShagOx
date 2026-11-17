using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Deliveries.Translations;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class BaseAppDbContext
    : DbContext
{
    public DbSet<DeliveryProviderTranslation>
        DeliveryProviderTranslations
    { get; set; }
}
