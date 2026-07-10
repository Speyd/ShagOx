using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Images.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Images;
public class ImageQueryRepository : BaseRepository, IImageQueryRepository
{
    public ImageQueryRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<Image?> GetByIdAsync(int id)
    {
        return await _db.Images
            .WithIncludes()
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<List<Image>> GetByIdsAsync(
        List<int> ids)
    {
        return await _db.Images
           .WithIncludes()
           .Where(x => ids.Contains(x.Id))
           .ToListAsync();
    }

    public async Task<List<Image>> GetByAdvertisementIdAsync(
        int advertId)
    {
        return await _db.Images
          .WithIncludes()
          .Where(x => x.AdvertisementId == advertId)
          .ToListAsync();
    }

    public async Task<int> GetNextOrder(
        int advertId,
        int? requestedOrder = null)
    {
        var orders = await _db.Images
             .Where(x => x.AdvertisementId == advertId)
             .Select(x => x.Order)
             .ToListAsync();


        if (!orders.Contains(requestedOrder ?? 0))
            return requestedOrder ?? 0;


        return orders
            .DefaultIfEmpty(0)
            .Max() + 1;
    }
}