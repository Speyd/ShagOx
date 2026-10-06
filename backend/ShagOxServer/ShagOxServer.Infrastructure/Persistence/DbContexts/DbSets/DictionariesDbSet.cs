using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class BaseAppDbContext
    : DbContext
{
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Category> Categories { get; set; }

    #region Attributes

    public DbSet<AttributeDefinition> AttributeDefinitions { get; set; }
    public DbSet<AttributeDictionary> AttributeDictionaries { get; set; }
    public DbSet<AttributeDictionaryValue> AttributeDictionaryValues { get; set; }

    #endregion
}