using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Avatars;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Avatars;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars.Query;
public partial class AvatarQueryRepository
    : QueryRepository<Avatar, AvatarSearchFilter>,
      IAvatarQueryRepository
{
    public AvatarQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<Avatar?> GetByUserIdAsync(
        long userId)
    {
        return await _db.Avatars
          .WithIncludes()
          .FirstOrDefaultAsync(x => x.UserId == userId);
    }
}