using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Verifications;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class AppDbContext : DbContext
{
    public DbSet<VerificationCode> VerificationCodes { get; set; }
}