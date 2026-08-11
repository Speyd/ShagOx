using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses;
public class StatusExistsRepository
    : ExistsRepository<Status>,
      IStatusExistsRepository
{
    public StatusExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsByCodeAsync(string code)
    {
        return await _db.Statuses
            .AnyAsync(x => x.Code == code);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _db.Statuses
            .AnyAsync(x => x.Name == name);
    }
}