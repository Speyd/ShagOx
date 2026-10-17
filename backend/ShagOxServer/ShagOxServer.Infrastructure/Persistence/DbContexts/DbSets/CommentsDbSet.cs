using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts.DbSets;
public partial class BaseAppDbContext
    : DbContext
{
    public DbSet<ReviewComment> ReviewComments { get; set; }
    public DbSet<ReviewRepliComment> ReviewRepliComments { get; set; }

}
