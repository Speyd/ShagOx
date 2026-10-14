using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Images;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Query;
public partial class ImageQueryRepository 
    : SearchRepository<Image, ImageSearchFilter>, 
      IImageQueryRepository
{
    public ImageQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


     public async Task<List<Image>> GetByIdsAsync(
        List<long> ids,
        CancellationToken cancellationToken = default)
    {
        return await _db.Images
           .WithIncludes()
           .Where(x => ids.Contains(x.Id))
           .ToListAsync(cancellationToken);
    }

     public async Task<List<Image>> GetByAdvertisementAsync(
        long advertId,
        CancellationToken cancellationToken = default)
    {
        return await _db.Images
          .WithIncludes()
          .Where(x => x.AdvertisementId == advertId)
          .ToListAsync(cancellationToken);
    }

    public async Task<int> GetNextOrder(
        long advertId,
        int? requestedOrder = null,
        CancellationToken cancellationToken = default)
    {
        var orders = await _db.Images
             .Where(x => x.AdvertisementId == advertId)
             .Select(x => x.Order)
             .ToListAsync(cancellationToken);


        if (!orders.Contains(requestedOrder ?? 0))
            return requestedOrder ?? 0;


        return orders
            .DefaultIfEmpty(0)
            .Max() + 1;
    }
}
