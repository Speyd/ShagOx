using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class AppDbContext : DbContext
{
    public DbSet<Condition> Conditions { get; set; }
    public DbSet<Currency> Currencies { get; set; }

    public DbSet<Image> Images { get; set; }
    public DbSet<Avatar> Avatars { get; set; }
}