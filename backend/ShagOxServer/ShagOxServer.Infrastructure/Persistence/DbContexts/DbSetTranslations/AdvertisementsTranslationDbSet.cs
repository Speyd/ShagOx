using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Localizations.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class AppDbContext 
    : DbContext
{
    public DbSet<StatusTranslation> StatusTranslations { get; set; }
}