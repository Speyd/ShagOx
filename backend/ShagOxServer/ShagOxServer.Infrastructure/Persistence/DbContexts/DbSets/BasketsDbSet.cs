using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Entities.LineItems;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class BaseAppDbContext
    : DbContext
{
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }

}
