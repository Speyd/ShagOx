using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core;
public class AdvertisementExistsRepository 
    : ExistsRepository<Advertisement>, 
      IAdvertisementExistsRepository
{
    public AdvertisementExistsRepository(
        ReplicaDbContext db)
        : base(db)
    { }

    public async Task<bool> IsOwnerAsync(
        long adId,
        long userId)
    {
        var result = await _db.Advertisements
           .AnyAsync(x =>
              (x.Id == adId && x.SellerId == userId));

        return result;
    }
}