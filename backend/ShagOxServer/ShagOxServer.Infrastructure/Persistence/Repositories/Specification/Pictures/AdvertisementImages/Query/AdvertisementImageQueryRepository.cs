using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Images;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Query;
public partial class AdvertisementImageQueryRepository 
    : SearchRepository<AdvertisementImage, ImageSearchFilter>, 
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

    public async Task<List<AdvertisementImage>> GetByOwnerAsync(
        long ownerId,
        CancellationToken cancellationToken = default)
    {
        return await _db.AdvertisementImages
          .WithIncludes()
          .Where(x => x.OwnerId == ownerId)
          .ToListAsync(cancellationToken);
    }

    public async Task<int> GetNextOrder(
        long advertId,
        int? requestedOrder = null,
        CancellationToken cancellationToken = default)
    {
        var orders = await _db.AdvertisementImages
             .Where(x => x.OwnerId == advertId)
             .Select(x => x.Order)
             .ToListAsync(cancellationToken);


        if (!orders.Contains(requestedOrder ?? 0))
            return requestedOrder ?? 0;


        return orders
            .DefaultIfEmpty(0)
            .Max() + 1;
    }
}
