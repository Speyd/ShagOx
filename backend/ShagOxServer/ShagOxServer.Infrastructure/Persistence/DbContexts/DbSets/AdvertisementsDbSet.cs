using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class AppDbContext : DbContext
{
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Advertisement> Advertisements { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
}