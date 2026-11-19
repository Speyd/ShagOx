using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Payments;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public partial class BaseAppDbContext
    : DbContext
{
    public DbSet<PaymentWebhook> PaymentWebhooks { get; set; }
}
