using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Entities.Specification;

public class AppDbContext : DbContext
{
    public DbSet<Region> Regions { get; set; }
    public DbSet<City> Cities { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }


    public DbSet<Category> Categories { get; set; }
    public DbSet<AttributeDefinition> AttributeDefinitions { get; set; }

    public DbSet<Condition> Conditions { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Image> Images { get; set; }

    public DbSet<Advertisement> Advertisements { get; set; }



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