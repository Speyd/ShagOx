using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Images;
public class ImageExistsRepository : BaseRepository, IImageExistsRepository
{
    public ImageExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _db.Images
            .AnyAsync(x => x.Id == id);
    }
}