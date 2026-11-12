using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts.DbSets;
public partial class BaseAppDbContext
    : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}
