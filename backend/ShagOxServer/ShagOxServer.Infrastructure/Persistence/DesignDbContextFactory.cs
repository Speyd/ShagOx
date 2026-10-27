using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
// Вкажіть точний namespace вашого AppDbContext

namespace ShagOxServer.Infrastructure.Persistence;

public class DesignDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Пряме підключення до первинної бази postgres-primary (порт 5433)
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=shagox;Username=postgres;Password=U/%QyNj9hahk+9U;TimeZone=Europe/Kyiv");

        return new AppDbContext(optionsBuilder.Options);
    }
}
