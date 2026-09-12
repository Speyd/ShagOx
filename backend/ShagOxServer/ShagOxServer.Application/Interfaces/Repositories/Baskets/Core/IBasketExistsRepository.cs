using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
public interface IBasketExistsRepository
    : IExistsRepository<Basket>
{
    Task<bool> ExistsByUserAsync(
        int userId);
}