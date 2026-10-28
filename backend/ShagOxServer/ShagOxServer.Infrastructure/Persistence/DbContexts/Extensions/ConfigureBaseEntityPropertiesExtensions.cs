using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using System.Reflection;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public static class ConfigureBaseEntityPropertiesExtensions
{
    public static void ConfigureBaseEntityProperties(this ModelBuilder modelBuilder)
    {
        var derivedTypes = modelBuilder.Model
            .GetEntityTypes()
            .Where(e =>
                e.ClrType != typeof(BaseEntity) &&
                !e.IsOwned() &&
                e.BaseType == null &&
                typeof(BaseEntity).IsAssignableFrom(e.ClrType))
            .Select(e => e.ClrType)
            .ToList();

        var method = typeof(ConfigureBaseEntityPropertiesExtensions)
            .GetMethod(nameof(ConfigureEntity),
                BindingFlags.NonPublic | BindingFlags.Static)!;

        foreach (var clrType in derivedTypes)
        {
            method.MakeGenericMethod(clrType)
                  .Invoke(null, new object[] { modelBuilder });
        }
    }

    private static void ConfigureEntity<TEntity>(ModelBuilder modelBuilder)
        where TEntity : BaseEntity
    {
        modelBuilder.Entity<TEntity>(b =>
        {
            b.Property(e => e.EntityStatus)
             .HasConversion<string>()
             .HasDefaultValue(EntityStatus.Active)
             .IsRequired();
        });
    }
}
