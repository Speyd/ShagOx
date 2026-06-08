using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Location;

public class AppDbContext : DbContext
{
    DbSet<Region> Regions { get; set; }
    DbSet<City> Cities { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}