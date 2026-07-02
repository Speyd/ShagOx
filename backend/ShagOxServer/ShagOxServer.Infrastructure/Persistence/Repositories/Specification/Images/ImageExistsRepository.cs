using ShagOxServer.Infrastructure.Interfaces.Specification.Images;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Images;
public class ImageExistsRepository : BaseRepository, IImageExistsRepository
{
    public ImageExistsRepository(AppDbContext db)
        : base(db)
    { }
}
