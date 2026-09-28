using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Avatars;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars;

public class AvatarQueryRepository
    : QueryRepository<Avatar, AvatarSearchFilter>,
      IAvatarQueryRepository
{
    public AvatarQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<Avatar> ApplyIncludes(
        IQueryable<Avatar> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<Avatar> ApplyFilter(
        IQueryable<Avatar> query,
        AvatarSearchFilter filter)
    {
        return query.Filter(filter);
    }

    public async Task<Avatar?> GetByUserIdAsync(
        long userId)
    {
        return await _db.Avatars
          .WithIncludes()
          .FirstOrDefaultAsync(x => x.UserId == userId);
    }
}