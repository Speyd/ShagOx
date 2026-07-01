using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Interfaces.Specification;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification;
public class ImageRepository : BaseRepository, IImageRepository
{
    public ImageRepository(AppDbContext db)
        : base(db)
    { }

    public async Task AddAsync(Image image)
    {
        await _db.Images.AddAsync(image);

        await _db.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Image image)
    {
        _db.Images.Update(image);

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAsync(Image image)
    {
        _db.Images.Remove(image);

        await _db.SaveChangesAsync();
    }

    private IQueryable<Image> Query()
    {
        return _db.Images
           .Include(x => x.Advertisement);
    }

    public async Task<Image?> GetByIdAsync(int id)
    {
        return await Query()
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<List<Image>> GetByIdsAsync(
        List<int> ids)
    {
        return await Query()
           .Where(x => ids.Contains(x.Id))
           .ToListAsync();
    }
}
