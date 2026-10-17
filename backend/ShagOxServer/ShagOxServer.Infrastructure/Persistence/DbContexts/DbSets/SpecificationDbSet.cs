using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class BaseAppDbContext
    : DbContext
{
    public DbSet<Condition> Conditions { get; set; }
    public DbSet<Currency> Currencies { get; set; }

    public DbSet<ReviewImage> ReviewImages { get; set; }
    public DbSet<ReviewRepliImage> ReviewRepliImages { get; set; }

    public DbSet<AdvertisementImage> AdvertisementImages { get; set; }
    public DbSet<Avatar> Avatars { get; set; }
}
