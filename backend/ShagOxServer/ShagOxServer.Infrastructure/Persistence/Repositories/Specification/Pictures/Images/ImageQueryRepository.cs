using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Images.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Images;
public class ImageQueryRepository 
    : QueryRepository<Image>, 
      IImageQueryRepository
{
    public ImageQueryRepository(AppDbContext db)
        : base(db)
    { }


    public override async Task<Image?> GetByIdAsync(
        int id)
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

    public override async Task<PagedResult<Image>> GetPagedAsync(
      PaginationParams pagination)
    {
        return await _db.Images
            .WithIncludes()
            .ToPagedResultAsync(pagination);
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