using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.AdvertisementImages;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core.Multiple.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Query;
public partial class AdvertisementImageQueryRepository 
    : PictureOrderQueryRepository<AdvertisementImage, AdvertisementImageSearchFilter>, 
      IAdvertisementImageQueryRepository
{
    public AdvertisementImageQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<List<AdvertisementImage>> GetByIdsAsync(
        List<long> ids,
        CancellationToken cancellationToken = default)
    {
        return await _db.AdvertisementImages
           .WithIncludes()
           .Where(x => ids.Contains(x.Id))
           .ToListAsync(cancellationToken);
    }
}
