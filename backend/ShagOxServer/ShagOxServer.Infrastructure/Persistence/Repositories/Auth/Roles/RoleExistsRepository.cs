using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles;
public class RoleExistsRepository
    : ExistsRepository<Role>,
      IRoleExistsRepository
{
    public RoleExistsRepository(
        AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Roles
            .WithStatusFilter(status)
            .AnyAsync(r => r.Name == name,
                cancellationToken);
    }
}
