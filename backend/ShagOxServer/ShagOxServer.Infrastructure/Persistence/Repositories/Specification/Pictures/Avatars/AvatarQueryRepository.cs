using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars;

public class AvatarQueryRepository
    : QueryRepository<Avatar>,
      IAvatarQueryRepository
{
    public AvatarQueryRepository(AppDbContext db)
        : base(db)
    { }


    public override async Task<Avatar?> GetByIdAsync(
        int id)
    {
        return await _db.Avatars
            .WithIncludes()
            .FirstOrDefaultAsync(i => i.Id == id);
    }


    public override async Task<PagedResult<Avatar>> GetPagedAsync(
      PaginationParams pagination)
    {
        return await _db.Avatars
            .WithIncludes()
            .ToPagedResultAsync(pagination);
    }

    public async Task<Avatar?> GetByUserIdAsync(
        int userId)
    {
        return await _db.Avatars
          .WithIncludes()
          .FirstOrDefaultAsync(x => x.UserId == userId);
    }
}