using Microsoft.EntityFrameworkCore;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
public partial class AppDbContext 
    : BaseAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}