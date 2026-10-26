using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public static class ConfigureBaseEntityPropertiesExtensions
{
    public static void ConfigureBaseEntityProperties(
        this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                continue;

            modelBuilder.Entity<BaseEntity>()
               .Property(x => x.EntityStatus)
               .HasConversion<string>()
               .HasDefaultValue(EntityStatus.Active)
               .IsRequired();
        }
    }
}
