using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Devices;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class BaseAppDbContext
    : DbContext
{
    public DbSet<Device> Devices { get; set; }

}
