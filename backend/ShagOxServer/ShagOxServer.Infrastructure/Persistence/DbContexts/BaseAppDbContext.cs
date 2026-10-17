using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using ShagOxServer.Domain.Base;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public abstract partial class BaseAppDbContext : DbContext
{
    protected BaseAppDbContext(
        DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(BaseAppDbContext).Assembly);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<uint>("xmin")
                    .IsRowVersion();
            }
        }
    }
}
