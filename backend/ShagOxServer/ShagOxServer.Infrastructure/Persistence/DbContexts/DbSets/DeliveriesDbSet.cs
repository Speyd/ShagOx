using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Deliveries;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class BaseAppDbContext
    : DbContext
{
    public DbSet<DeliveryProvider> DeliveryProviders { get; set; }

}
