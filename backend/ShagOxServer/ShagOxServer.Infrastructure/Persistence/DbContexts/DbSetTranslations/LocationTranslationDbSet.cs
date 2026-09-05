using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class AppDbContext 
    : DbContext
{
    public DbSet<RegionTranslation> RegionTranslations { get; set; }
    public DbSet<CityTranslation> CityTranslations { get; set; }

}