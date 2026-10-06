using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class BaseAppDbContext
    : DbContext
{
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<BasketAttribute> BasketAttributes { get; set; }

}