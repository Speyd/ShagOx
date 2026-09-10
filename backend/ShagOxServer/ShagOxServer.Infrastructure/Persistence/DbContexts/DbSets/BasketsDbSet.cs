using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class AppDbContext : DbContext
{
    public DbSet<BasketAttribute> BasketAttributes { get; set; }
}