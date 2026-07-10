using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Domain.Entities.Specification;

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

    public void AddAsync(Image image)
    {
        _db.Images.Add(image);
    }

    public bool UpdateAsync(Image image)
    {
        _db.Images.Update(image);
        return true;
    }

    public void DeleteAsync(Image image)
    {
        _db.Images.Remove(image);
    }
}