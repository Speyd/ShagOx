using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Interfaces.Specification.Images;
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
}