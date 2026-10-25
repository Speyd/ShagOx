using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Avatars;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Avatars;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars.Query;
public partial class AvatarQueryRepository
    : PictureQueryRepository<Avatar, AvatarSearchFilter>,
      IAvatarQueryRepository
{
    public AvatarQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }
}
