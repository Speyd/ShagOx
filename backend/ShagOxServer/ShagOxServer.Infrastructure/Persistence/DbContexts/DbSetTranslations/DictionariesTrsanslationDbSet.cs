using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class AppDbContext : DbContext
{
    public DbSet<AttributeDefinitionTranslation>
        AttributeDefinitionTranslations { get; set; }

    public DbSet<ProductTypeTranslation>
        ProductTypeTranslations { get; set; }
}