using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification.Translations;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;

public partial class AppDbContext : DbContext
{
    public DbSet<ConditionTranslation> ConditionTranslations { get; set; }
}