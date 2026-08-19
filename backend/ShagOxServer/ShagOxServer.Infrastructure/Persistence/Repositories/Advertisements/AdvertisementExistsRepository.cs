using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements;
public class AdvertisementExistsRepository 
    : ExistsRepository<Advertisement>, 
      IAdvertisementExistsRepository
{
    public AdvertisementExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> IsOwnerAsync(
        int adId,
        int userId)
    {
        var result = await _db.Advertisements
           .AnyAsync(x =>
              (x.Id == adId && x.SellerId == userId));

        return result;
    }
}