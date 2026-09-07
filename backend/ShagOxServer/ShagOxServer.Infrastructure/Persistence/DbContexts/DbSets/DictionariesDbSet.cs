using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class AppDbContext : DbContext
{
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<AttributeDefinition> AttributeDefinitions { get; set; }
}