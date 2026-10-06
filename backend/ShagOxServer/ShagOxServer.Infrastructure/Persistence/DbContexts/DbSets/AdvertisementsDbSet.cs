using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class BaseAppDbContext
    : DbContext
{
    public DbSet<Advertisement> Advertisements { get; set; }
    public DbSet<AdvertisementVariant> AdvertisementVariants { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
}