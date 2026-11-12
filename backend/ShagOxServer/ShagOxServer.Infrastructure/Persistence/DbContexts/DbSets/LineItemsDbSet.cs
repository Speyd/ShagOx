using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.LineItems;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class BaseAppDbContext
    : DbContext
{
    public DbSet<LineItemAttribute> LineItemAttributes { get; set; }

}
