using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;

namespace ShagOxServer.Application.Interfaces.Services.Base.Query;
public interface IQueryService<TDto, TEntity, TFilter>
    : IQueryOperations<TDto, TFilter>
    where TDto: BaseDto
    where TEntity: BaseEntity
    where TFilter: BaseFilter
{
    Task<TDto> ApplyMapperAsync(
      TEntity entity);
}
