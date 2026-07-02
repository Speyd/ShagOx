using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Interfaces.Specification.Images;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Images;
public class ImageRepository : BaseRepository, IImageRepository
{
    public ImageRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<Image?> GetByIdAsync(int id)
    {
        return await _db.Images
            .FirstOrDefaultAsync(i => i.Id == id);
    }

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
}
