using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Validator;
public class FavoriteValidator
    : BaseValidator<Favorite>
{
    public FavoriteValidator(
        IQueryRepository<Favorite> favoriteRepository,
        IExistsRepository<Favorite> favoriteExistsRepository
    ) : base(favoriteRepository, favoriteExistsRepository)
    {
    }
}
