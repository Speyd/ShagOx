using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class BaseAppDbContext
    : DbContext
{
    public DbSet<StatusTranslation> StatusTranslations { get; set; }
}