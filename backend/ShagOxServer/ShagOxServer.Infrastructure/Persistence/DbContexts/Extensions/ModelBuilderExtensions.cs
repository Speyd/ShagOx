using Microsoft.EntityFrameworkCore;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyGlobalNamingConvention(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var fk in entity.GetForeignKeys())
            {
                var tableName = entity.GetTableName();
                var principalTableName = fk.PrincipalEntityType.GetTableName();
                var propertyNames = string.Join("_", fk.Properties.Select(p => p.Name));

                fk.SetConstraintName($"FK_{tableName}_{principalTableName}_{propertyNames}");
            }

            foreach (var index in entity.GetIndexes())
            {
                var tableName = entity.GetTableName();
                var propertyNames = string.Join("_", index.Properties.Select(p => p.Name));

                index.SetDatabaseName($"IX_{tableName}_{propertyNames}");
            }
        }
    }
}
