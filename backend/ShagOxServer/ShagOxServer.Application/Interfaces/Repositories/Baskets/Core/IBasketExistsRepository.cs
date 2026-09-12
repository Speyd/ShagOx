using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
public interface IBasketExistsRepository
    : IExistsRepository<Basket>, IExistsOwnerRepository
{
    Task<bool> ExistsByUserAsync(
        int userId);
}