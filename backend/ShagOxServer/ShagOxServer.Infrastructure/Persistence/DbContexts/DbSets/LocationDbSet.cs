using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class BaseAppDbContext
    : DbContext
{
    public DbSet<Region> Regions { get; set; }
    public DbSet<City> Cities { get; set; }
}