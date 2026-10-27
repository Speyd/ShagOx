using Microsoft.EntityFrameworkCore;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public abstract partial class BaseAppDbContext 
    : DbContext
{
    protected BaseAppDbContext(
        DbContextOptions options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(BaseAppDbContext).Assembly);

        modelBuilder.ApplyGlobalNamingConvention();

        modelBuilder.ConfigureGlobalQueryFilters();

        modelBuilder.ConfigureBaseEntityProperties();
    }
}
