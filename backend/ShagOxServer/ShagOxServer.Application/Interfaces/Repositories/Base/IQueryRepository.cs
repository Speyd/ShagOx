using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Base;
public interface IQueryRepository <T>
{
    Task<T?> GetByIdAsync(int id);

    Task<PagedResult<T>> GetPagedAsync(
        PaginationParams pagination);
}