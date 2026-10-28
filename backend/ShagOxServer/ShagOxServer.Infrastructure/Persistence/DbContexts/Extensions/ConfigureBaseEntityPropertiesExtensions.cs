using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public static class ConfigureBaseEntityPropertiesExtensions
{
    public static void ConfigureBaseEntityProperties(
        this ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model
            .GetEntityTypes()
            .Where(entityType =>
                entityType.ClrType != typeof(BaseEntity) &&
                typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            .ToList();

        foreach (var entityType in entityTypes)
        {
            modelBuilder.Entity(entityType.ClrType)
             .Property(nameof(BaseEntity.EntityStatus))
             .HasConversion<string>()
             .HasDefaultValue(EntityStatus.Active)
             .IsRequired();
        }
    }
}
