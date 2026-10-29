using ShagOxServer.Application.DTOs.Dictionaries.Attributes.Core.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeValues;
public interface IAttributeValueQueryService
{
    Task<Result<List<AttributeDto>>> GetValuesByCategoryAsync(
            long categoryId,
            PaginationParams pagination,
            CancellationToken cancellationToken = default);
}
