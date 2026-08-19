using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class AppDbContext : DbContext
{
    public DbSet<Condition> Conditions { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Image> Images { get; set; }
}